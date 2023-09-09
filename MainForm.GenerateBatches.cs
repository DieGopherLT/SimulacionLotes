using Bogus;

namespace SimulacionLotes
{
    using Batch = Queue<Process>;
    public partial class MainForm : Form
    {
        /*
         * Esta función genera la cola de colas que proceso que uso para simular los lotes.
         * Una cola de procesos es el equivalente de un lote.
         * Los lotes van en una cola para fácilmente ejecutarlos en el órden que se crearon.
         */
        private ValueTuple<Queue<Batch>, string> GenerateBatches(int processesAmount)
        {
            const int LIMIT_PER_BATCH = 5;
            int numberOfBatches = CalculateAmountOfBatches(processesAmount, LIMIT_PER_BATCH);

            Queue<Batch> batches = new Queue<Batch>(numberOfBatches);
            List<int> batchSizes = GenerateBatchesSizes(processesAmount, LIMIT_PER_BATCH);

            int processId = 1;
            int batchNumber = 1;
            string documentText = string.Empty;
            foreach (int batchSize in batchSizes)
            {
                Batch batch = new Batch(LIMIT_PER_BATCH);
                documentText += $"********** Lote {batchNumber} **********\n";
                for (int i = 0; i < batchSize; i++)
                {
                    Process process = GenerateProcessRandomly(processId);
                    batch.Enqueue(process);
                    documentText += process.ToString() + Environment.NewLine;
                    processId++;
                }
                batches.Enqueue(batch);
                batchNumber++;
            }

            return (batches, documentText);
        }

        /*
         * Esta función calcula el número de lotes que necesito con base en los procesos que se generarán
         * y el límite de procesos por lote.
         * 
         * El límite de procesos por lote es de 5.
         */
        private int CalculateAmountOfBatches(int processesAmount, int batchLimit)
        {
            double batchesToGenerate = (double)processesAmount / batchLimit;
            bool resultHasDecimal = batchesToGenerate % 1 != 0;

            if (resultHasDecimal)
            {
                batchesToGenerate += 1;
                batchesToGenerate = Math.Floor(batchesToGenerate);
            }

            int numberOfBatches = Convert.ToInt32(batchesToGenerate);
            return numberOfBatches;
        }

        /*
         * Esta función la hice para facilitarme en rellenar bien los lotes.
         * Supongamos que le pedi 8 procesos al programa...
         * 
         * Esta función genera un arreglo de números que representa la cantidad de elementos que tocan por lote,
         * si dijimos que la entrada es 8 y el límite es 5 por lote;
         * entonces genera un arreglo tal que:
         * 
         * [5, 3]
         * 
         * El primer lote va lleno mientras que el segundo lleva 3
         * 
         * Si fuera 17 el número de procesos...
         * 
         * [5, 5, 5, 2]
         */
        private List<int> GenerateBatchesSizes(int processesAmount, int limitPerBatch)
        {
            List<int> sizes = new List<int>();

            while (processesAmount > 0)
            {
                sizes.Add(Math.Min(limitPerBatch, processesAmount));
                processesAmount -= limitPerBatch;
            }

            return sizes;
        }

        /*
         * Uno de los requerimientos de la actividad era el de generar los procesos de manera completamente aleatoria
         * 
         * El nombre se genera con una librería de terceros mientras que la operación y el TME con números aleatorios.
         */
        private Process GenerateProcessRandomly(int processId)
        {
            Faker faker = new();

            string[] operations = { "+", "-", "*", "/" };
            string randomOperation = operations.ElementAt(numberGenerator.Next(0, 4));
            int firstOperand = numberGenerator.Next(1, 11);
            int secondOperand = numberGenerator.Next(1, 11);

            int randomTME = numberGenerator.Next(4, 14);

            return new Process
            {
                ID = processId,
                ProgrammerName = $"{faker.Name.FirstName()}",
                Operation = $"{firstOperand} {randomOperation} {secondOperand}",
                TME = randomTME,
            };
        }
    }
}
