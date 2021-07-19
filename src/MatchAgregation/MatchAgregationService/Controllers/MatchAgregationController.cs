using System;
using System.Linq;
using System.Threading.Tasks;
using MatchAgregationService.Models;
using MatchAgregationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MatchAgregationService.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class MatchAgregationController : ControllerBase
    {
        private readonly ILogger<MatchAgregationController> _logger;
        private readonly IMatchesResultLoader _resultLoader;
        private readonly ITeamStatistic _statistic;

        public MatchAgregationController(ILogger<MatchAgregationController> logger, ITeamStatistic statistic,
            IMatchesResultLoader resultLoader)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _statistic = statistic ?? throw new ArgumentNullException(nameof(statistic));
            _resultLoader = resultLoader ?? throw new ArgumentNullException(nameof(resultLoader));
        }

        public async Task<IActionResult> Agregate()
        {
            _logger.LogInformation("Aggregation request");
            await _resultLoader.LoadMatches();
            var teamStatistic = _statistic.GetTeamStatistics().ToList();
            if (teamStatistic.Count == 0)
            {
                return Problem(detail: "No teams found");
            }

            var mostWin = teamStatistic.OrderByDescending(s => s.Wins).First();
            var mostScored = teamStatistic.OrderByDescending(s => s.ScoredPerGame).First();
            var lessReceived = teamStatistic.OrderBy(s => s.ReceivedPerGame).First();

            var aggregated = new SportResult
            {
                MostWin = new TeamData() { Name = mostWin.Team, Amount = mostWin.Wins },
                LessReceivedPerGame = new TeamData {Name = lessReceived.Team, Amount = lessReceived.ReceivedPerGame},
                MostScoredPerGame = new TeamData {Name = mostScored.Team, Amount = mostScored.ScoredPerGame}
            };
            _logger.LogInformation(aggregated.ToString());
            return Ok(aggregated);
        }
    }
}