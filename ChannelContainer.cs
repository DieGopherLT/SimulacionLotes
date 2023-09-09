using System.Threading.Channels;

namespace SimulacionLotes
{
    internal class ChannelContainer
    {
        public required Channel<int> PendingBatches { get; set; }
        public required Channel<int> GlobalClock { get; set; }
        public required Channel<string> OnWaitingProcesses { get; set; }
        public required Channel<string> OnExecutingProcess { get; set; }
        public required Channel<string> OnFinishedProcesses { get; set; }

        public void CloseChannels()
        {
            PendingBatches.Writer.Complete();
            GlobalClock.Writer.Complete();
            OnWaitingProcesses.Writer.Complete();
            OnExecutingProcess.Writer.Complete();
            OnFinishedProcesses.Writer.Complete();
        }
    }
}
