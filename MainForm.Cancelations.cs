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
        static CancellationTokenSource cts = new CancellationTokenSource();

        private void CancelationButton_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
