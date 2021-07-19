using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MatchAgregationService.Services
{
    internal class MatchResultClient : IMatchResultClient
    {
        private readonly ILogger<MatchResultClient> _logger;
        private readonly IResultParser _resultParser;
        private readonly ITeamResultStorage _teamResultStorage;

        public MatchResultClient(ILogger<MatchResultClient> logger, ITeamResultStorage teamResultStorage,
            IResultParser resultParser)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _teamResultStorage = teamResultStorage ?? throw new ArgumentNullException(nameof(teamResultStorage));
            _resultParser = resultParser;
        }

        public async Task LoadResults(string parameterString)
        {
            _logger.LogTrace($"{nameof(LoadResults)}: [{parameterString}]");
            using var client = new WebClient();
            var result =
                await client.DownloadStringTaskAsync(parameterString);
            _logger.LogInformation(result);
            foreach (var teamResult in _resultParser.Parse(result)) _teamResultStorage.AddResult(teamResult);
        }
    }
}