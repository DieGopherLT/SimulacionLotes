using System.Threading.Channels;

namespace SimulacionLotes
{
    using Batch = Queue<Process>;
    // En este partial tengo toda la lógica prinicpal relacionada al procesamiento por lotes
    public partial class MainForm : Form
    {
        private ChannelContainer CreateChannels()
        {
            // Canales, conductos por donde los datos pueden pasar de manera segura entre hilos
            Channel<int> pendingBatchesChannel = Channel.CreateUnbounded<int>();
            Channel<int> globalClockChannel = Channel.CreateUnbounded<int>();
            Channel<string> onWaitingProcessesChannel = Channel.CreateUnbounded<string>();
            Channel<string> onExecutingProcessesChannel = Channel.CreateUnbounded<string>();
            Channel<string> onFinishedProcessesChannel = Channel.CreateUnbounded<string>();

            // Para facilitar el paso de varios canales de una, hice esta clase
            ChannelContainer container = new ChannelContainer
            {
                PendingBatches = pendingBatchesChannel,
                GlobalClock = globalClockChannel,
                OnWaitingProcesses = onWaitingProcessesChannel,
                OnExecutingProcess = onExecutingProcessesChannel,
                OnFinishedProcesses = onFinishedProcessesChannel
            };

            return container;
        }

        private async void StartBatchingProcessing(Queue<Batch> batches)
        {
            ChannelContainer container = CreateChannels();

            // Mandamos a llamar tareas para que se hagan en segundo plano, podemos considerarlos como otros hilos
            // A diferencia de la clase Thread, estos son más sencillos de usar y eficientes, pero menos controlables
            Task batchingHandler = Task.Run(() => HandleBatches(batches, container));

            // Todos los de aqui abajo son para actualizar la UI
            Task updateGlobalClock = Task.Run(() => UpdateGlobalClockTask(container.GlobalClock));
            Task updatePendingBatches = Task.Run(() => UpdatePendingBatchesTask(container.PendingBatches));

            Task updateExecutionProcesses = Task.Run(() => UpdateOnExecutionProcessTask(container.OnExecutingProcess));
            Task updateWaitingProcesses = Task.Run(() => UpdateOnWaitingProcessTask(container.OnWaitingProcesses));
            Task updateFinishingProcess = Task.Run(() => UpdateOnFinishedProcessTask(container.OnFinishedProcesses));

            CancellationButton.Enabled = true;
            InterruptButton.Enabled = true;

            // Bloqueamos la ejecución del programa hasta que todas estas tareas terminen
            await Task.WhenAll(
                batchingHandler, updateGlobalClock, 
                updatePendingBatches, updateExecutionProcesses, 
                updateWaitingProcesses, updateFinishingProcess
            );

            // Habilitamos el botón para obtener nuestro 'ticket' de los resultados de la operación
            GenerateResultsButton.Enabled = true;
            CancellationButton.Enabled = false;
            InterruptButton.Enabled = false;
        }

        /* 
         *  Esta función se encarga de todo lo relacionado a los lotes, para actualizar la UI, envía datos
         *  a los respectivos canales encargados de una parte de la UI.
         */
        private async Task HandleBatches(Queue<Batch> batches, ChannelContainer container)
        {
            int batchNumber = 1;
            while (batches.Count > 0) // Desencolamiento de lotes
            {
                Batch currentBatch = batches.Dequeue();
                string batchTitle = $"********* Lote {batchNumber} **********";

                await container.PendingBatches.Writer.WriteAsync(batches.Count);
                await container.OnFinishedProcesses.Writer.WriteAsync(batchTitle);
                await container.OnExecutingProcess.Writer.WriteAsync(string.Empty);
                await container.OnWaitingProcesses.Writer.WriteAsync($"\n\n\tCargando lote {batchNumber}...");
                await Task.Delay(1500);

                while (currentBatch.Count > 0) // Desencolamiento de procesos
                {
                    Process currentProcess = currentBatch.Dequeue();
                    string pendingProcesses = GenerateOnWaitProcessesText(currentBatch, batchTitle);

                    await container.OnWaitingProcesses.Writer.WriteAsync(pendingProcesses);
                    await container.OnExecutingProcess.Writer.WriteAsync(currentProcess.ToStringExecuting());

                    await ExecuteProcess(currentProcess, container);

                    // En caso de que haya una interrupción, el proceso se vuelve a encolar
                    if (interruptButtonCTS.Token.IsCancellationRequested)
                    {
                        currentBatch.Enqueue(currentProcess);
                        // Los tokens son de un solo uso, tras el uso he de reasignarlo a una nueva instancia
                        interruptButtonCTS = new();
                        continue;
                    }

                    /*
                     * Si el if de arriba se ejecuta, este código consiguiente no se ejecuta.
                     * 
                     * Solamente se ejecuta cuando un proceso termina, ya sea por error o 
                     * con normalidad.
                     */
                    currentProcess.Time = currentProcess.ExecutionTime;
                    await container.OnFinishedProcesses.Writer.WriteAsync(currentProcess.ToStringSolved());
                }
                batchNumber++;
            }
            await container.OnExecutingProcess.Writer.WriteAsync(string.Empty);
            container.CloseChannels();
        }

        /* Todas estas funciones que están en este formato, se ejecutan en un task/hilo dedicado y funcionan
         * como listeners, cuando un canal les manda información, ejecutan el código, pero mientras no reciben
         * nada, la ejecución se bloquea donde el await.
         * 
         * Esta tarea en particular se encarga de mandar llamadas a la UI para actualizar el 
         * label de lotes pendientes.
        */
        private async Task UpdatePendingBatchesTask(Channel<int> pendingBatchesChannel)
        {
            await foreach (int pendingBatches in pendingBatchesChannel.Reader.ReadAllAsync())
            {
                UpdatePendingBatchesLabel(pendingBatches);
            }
        }

        /*
         * Manda a llamar actualizaciones al reloj global en la UI.
         */
        private async Task UpdateGlobalClockTask(Channel<int> globalClockChannel)
        {
            await foreach(int update in globalClockChannel.Reader.ReadAllAsync())
            {
                UpdateGlobalClockLabel(update);
            }
        }

        /*
         * Manda a llamar actualizaciones del cuadro de procesos en ejecución en la UI.
         */
        private async Task UpdateOnExecutionProcessTask(Channel<string> onExecutingProcessChannel)
        {
            await foreach (string process in onExecutingProcessChannel.Reader.ReadAllAsync())
            {
                UpdateOnExecutionRTB(process);
            }
        }

        /*
         * Manda a llamar actualizaciones del cuadro de procesos en espera en la UI.
         */
        private async Task UpdateOnWaitingProcessTask(Channel<string> onWaitingProcessesChannel)
        {
            await foreach (string processes in onWaitingProcessesChannel.Reader.ReadAllAsync())
            {
                UpdateOnWaitRTB(processes);
            }
        }

        /*
         * Manda a llamar actualizaciones del cuadro de procesos finalizados en la UI.
         */
        private async Task UpdateOnFinishedProcessTask(Channel<string> onFinishedProcessesChannel)
        {
            await foreach (string process in onFinishedProcessesChannel.Reader.ReadAllAsync())
            {
                UpdateOnFinishRTB(process);
            }
        }

        /*
         * Genera el texto que aparece en el cuadro de procesos pendientes
         * Debido a que los procesos andan encolados, tengo que hacer un desencolado para poder
         * mostrarlos en pantalla.
         */
        private string GenerateOnWaitProcessesText(Batch processes, string batchTitle)
        {
            // Hago una copia del lote para no alterar la que se usa para el procesamiento
            Batch copy = new Batch(processes);
            string onWaitRtbText = string.Empty;

            if (copy.Count > 0)
            {
                onWaitRtbText += batchTitle + Environment.NewLine;
                onWaitRtbText += copy.Dequeue().ToStringPending();
                onWaitRtbText += $"\n\n\t{copy.Count} procesos pendientes";
            }

            return onWaitRtbText;
        }

        /*
         * La función más importante y que da vida a la simulación.
         * Aquí se genera el tiempo que le toma a un proceso completarse y se hacen las llamadas a los 
         * canales de actualización del reloj global y el proceso en ejecución.
         * 
         * Todo esto un delay de un segundo que hace que se vea que el programa ocurre segundo a segundo.
         */
        private async Task ExecuteProcess(Process process, ChannelContainer container)
        {
            const int INCREMENT = 1;
            const int DECREMENT = 1;

            int timeToFinishProcess = process.RemainingTime;

            // Los tokens de cancelación que provienen de sus respectivos sources
            CancellationToken cancelButtonCancellationToken = cancelButtonCTS.Token;
            CancellationToken interruptButtonCancellationToken = interruptButtonCTS.Token;

            for (int i = 0; i < timeToFinishProcess; i++)
            {
                if (cancelButtonCancellationToken.IsCancellationRequested) // Listeenr para cancelaciones
                {
                    process.HasError = true;
                    // Los tokens son de un solo uso, tras el uso he de reasignarlo a una nueva instancia
                    cancelButtonCTS = new();
                    break;
                }
                if (interruptButtonCancellationToken.IsCancellationRequested) // Listener para interrupciones
                {
                    break;
                }
                await Task.Delay(1000); 
                process.RemainingTime -= DECREMENT;
                process.ExecutionTime += INCREMENT;
                await container.OnExecutingProcess.Writer.WriteAsync(process.ToStringExecuting());
                await container.GlobalClock.Writer.WriteAsync(INCREMENT);
            }
        }
    }
}
