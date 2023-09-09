using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SimulacionLotes
{
    public partial class MainForm : Form
    {
        static CancellationTokenSource cancelButtonCTS = new();
        static CancellationTokenSource interruptButtonCTS = new();

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
