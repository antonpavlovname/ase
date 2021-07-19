using System;
using System.Collections.Concurrent;
using MatchAgregationServiceTests;

namespace MatchAgregationService.Services
{
    public interface ITeamResultStorage
    {
        void AddResult(TeamResult result);
    }

    internal class TeamResultStorage : ITeamResultStorage
    {
        private readonly ConcurrentDictionary<TeamResult, object> _teamResults =
            new ConcurrentDictionary<TeamResult, object>();

        private readonly ITeamStatistic _teamStatistic;

        public TeamResultStorage(ITeamStatistic teamStatistic)
        {
            _teamStatistic = teamStatistic ?? throw new ArgumentNullException(nameof(teamStatistic));
        }

        public void AddResult(TeamResult result)
        {
            if (_teamResults.TryAdd(result, null))
                _teamStatistic.Update(result.TeamName, result.Scored, result.Received);
        }
    }
}