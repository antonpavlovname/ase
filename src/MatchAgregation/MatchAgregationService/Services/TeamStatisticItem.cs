namespace MatchAgregationService.Services
{
    internal class TeamStatisticItem
    {
        private readonly object _syncLock = new object();
        public int Games { get; private set; }
        public int Wins { get; private set; }
        public int Scored { get; private set; }
        public int Received { get; private set; }

        public void Update(int scored, int received)
        {
            lock (_syncLock)
            {
                ++Games;
                if (scored > received) ++Wins;

                Scored += scored;
                Received += received;
            }
        }
    }
}