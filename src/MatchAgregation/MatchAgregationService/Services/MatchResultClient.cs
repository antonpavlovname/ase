using System;
using System.Net.Http;
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
            using var client = new HttpClient();
            string result = null;
            for (int attempt = 0; attempt < 5; ++attempt)
            {
                var response =
                    await client.GetAsync(parameterString);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    if (result.StartsWith("["))
                    {
                        break;
                    }
                }
                _logger.LogError($"StatusCode: [{response.StatusCode}], [{response.ReasonPhrase}], [{result}]");
                await Task.Delay(100);
            }

            if (result == null)
            {
                throw new InvalidOperationException($"No response from server: [{parameterString}]");
            }

            _logger.LogInformation(result);
            foreach (var teamResult in _resultParser.Parse(result)) _teamResultStorage.AddResult(teamResult);
        }
    }
}