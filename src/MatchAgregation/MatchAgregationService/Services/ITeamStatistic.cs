using System.Collections.Generic;

namespace MatchAgregationService.Services
{
    public interface ITeamStatistic
    {
        void Update(string team, int scored, int received);

        IEnumerable<TeamStatisticResult> GetTeamStatistics();
    }
}