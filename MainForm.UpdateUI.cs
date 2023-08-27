
namespace SimulacionLotes
{
    // En este partial puse todas las funciones de llamadas de actualización a la interfaz
    public partial class MainForm : Form
    {
        /*
         * Todas las funciones actualizan la UI, pero esta se maneja en un hilo diferente a los que uso en mis 
         * listenerse de los canales, todas las funciones están diseñadas para hacer llamadas a la UI de manera segura
         * entre hilos.
         * 
         * InvokeRequired verifica que quien llama a la UI se encuentre en el mismo hilo.
         * 
         * InvokeRequired es falso de primeras, por lo cual hace una llamada a la UI con Invoke y 
         * le mando la misma función, es decir, una llamada recursiva. 
         * Al ser recursivo, pues se vuelve a ejecutar, pero ahora sí es el hilo de la UI, por ende
         * ejecuta el código del else.
         */
        private void UpdatePendingBatchesLabel(int pendingBatches)
        {
            if (PendingBatchesLabel.InvokeRequired)
                PendingBatchesLabel.Invoke(new Action<int>(UpdatePendingBatchesLabel), pendingBatches); 
            else
                PendingBatchesLabel.Text = $"# Lotes pendientes: {pendingBatches}";
        }

        private void UpdateOnExecutionRTB(string process)
        {
            if (OnExecutionRTB.InvokeRequired)
                OnExecutionRTB.Invoke(new Action<string>(UpdateOnExecutionRTB), process);
            else
                OnExecutionRTB.Text = process;
        }

        private void UpdateOnWaitRTB(string processes)
        {
            if (onWaitRTB.InvokeRequired)
                onWaitRTB.Invoke(new Action<string>(UpdateOnWaitRTB), processes);
            else
                onWaitRTB.Text = processes;
        }

        private void UpdateOnFinishRTB(string process)
        {
            if (OnFinishedRTB.InvokeRequired)
                OnFinishedRTB.Invoke(new Action<string>(UpdateOnFinishRTB), process);  
            else
                OnFinishedRTB.AppendText(process + Environment.NewLine);
        }

        private void UpdateGlobalClockLabel(int increment)
        {
            if (SecondsPassedLabel.InvokeRequired)
                SecondsPassedLabel.Invoke(new Action<int>(UpdateGlobalClockLabel), increment);
            else
                SecondsPassedLabel.Text = $"{Convert.ToInt32(SecondsPassedLabel.Text) + increment}"; 
        }
    }
}
