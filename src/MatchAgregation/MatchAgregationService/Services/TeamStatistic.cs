using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MatchAgregationService.Services
{
    internal class TeamStatistic : ITeamStatistic
    {
        private readonly ConcurrentDictionary<string, TeamStatisticItem> _statistic =
            new ConcurrentDictionary<string, TeamStatisticItem>();

        public void Update(string team, int scored, int received)
        {
            var statisticItem = _statistic.GetOrAdd(team, _ => new TeamStatisticItem());
            statisticItem.Update(scored, received);
        }

        public IEnumerable<TeamStatisticResult> GetTeamStatistics()
        {
            return _statistic.Select(s => new TeamStatisticResult()
            {
                Team = s.Key,
                ReceivedPerGame = (double)s.Value.Received / s.Value.Games,
                ScoredPerGame = (double)s.Value.Scored / s.Value.Games,
                Wins =  s.Value.Wins
            });
        }
    }
}