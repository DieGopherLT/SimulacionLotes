namespace SimulacionLotes
{
    using Batch = Queue<Process>;
    public partial class MainForm : Form
    {
        Random numberGenerator = new Random();
        public MainForm()
        {
            InitializeComponent();
        }

        /*
         * Se ejecuta al presinar el botón de "Generar".
         */
        private void StartButton_Click(object sender, EventArgs e)
        {
            string processesAmount = ProcessesInput.Text;
            if (int.TryParse(processesAmount, out int processes))
            {
                DisableControls();
                (Queue<Batch> batches, string batchesDocument) = GenerateBatches(processes);
                WriteOnFile("lotes.txt", batchesDocument);
                StartBatchingProcessing(batches);
            }
            else
            {
                MessageBox.Show("Ingresa un número entero válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableControls()
        {
            StartButton.Enabled = false;
            ProcessesInput.Enabled = false;
        }

        /*
         * Se ejecuta al presinar el botón de "Obtener resultados".
         */
        private void GenerateResultsButton_Click(object sender, EventArgs e)
        {
            string resultsContent = OnFinishedRTB.Text;
            WriteOnFile("resultados.txt", resultsContent);
            ResetControls();
            MessageBox.Show("¡Resultados listos!");
        }

        private void ResetControls()
        {
            StartButton.Enabled = true;
            ProcessesInput.Enabled = true;
            ProcessesInput.Text = string.Empty;
            OnFinishedRTB.Text = string.Empty;
            SecondsPassedLabel.Text = "0";
            GenerateResultsButton.Enabled = false;
        }
    }
}