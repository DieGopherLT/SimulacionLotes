namespace SimulacionLotes
{
    public partial class MainForm : Form
    {
        // Tokens de cancelación, estos me permiten mandar señales para cancelar operaciones
        static CancellationTokenSource cancelButtonCTS = new();
        static CancellationTokenSource interruptButtonCTS = new();


        /*
         * Los handlers para clics de ambos botones nuevos para cancelar y bloquear respectivamente.
         * 
         * Cada uno tiene un token diferente porque tengo que realizar diferentes dependiendo de
         * cual se presinoe, de la misma manera que tengo diferentes listeners.
         */
        private void CancelationButton_Click(object sender, EventArgs e)
        {
            cancelButtonCTS.Cancel();
        }

        private void InterruptButton_Click(object obj, EventArgs e) 
        { 
            interruptButtonCTS.Cancel(); 
        }
    }
}
