using System.Threading.Channels;

namespace SimulacionLotes
{
    internal class ChannelContainer
    {
        public Channel<int> PendingBatches { get; set; }
        public Channel<int> GlobalClock { get; set; }
        public Channel<string> OnWaitingProcesses { get; set; }
        public Channel<string> OnExecutingProcess { get; set; }
        public Channel<string> OnFinishedProcesses { get; set; }

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
