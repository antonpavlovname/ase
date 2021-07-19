using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MatchAgregationService.Services
{
    internal class MatchesResultLoader : IMatchesResultLoader
    {
        private readonly IConfiguration _configuration;
        private readonly SemaphoreSlim _loading = new SemaphoreSlim(1);
        private readonly ILogger<MatchesResultLoader> _logger;
        private readonly IMatchResultClient _matchResultClient;
        private bool _loaded;

        public MatchesResultLoader(IConfiguration configuration, ILogger<MatchesResultLoader> logger,
            IMatchResultClient matchResultClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _matchResultClient = matchResultClient;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task LoadMatches()
        {
            try
            {
                await _loading.WaitAsync();
                if (_loaded)
                    return;
                var endPointsString = _configuration["MatchResultsEndpoints"];
                if (endPointsString == null)
                {
                    _logger.LogError(
                        $"Can't find MatchResultsEndpoints [{string.Join(",", _configuration.GetChildren().Select(ch => ch.Key))}]");
                    return;
                }

                var endpoints = JsonConvert.DeserializeObject<List<string>>(endPointsString);
                if (endpoints == null)
                {
                    _logger.LogError($"Can't find MatchResultsEndpoints [{_configuration["MatchResultsEndpoints"]}]");
                    return;
                }

                await Task.WhenAll(endpoints.Select(endpoint => _matchResultClient.LoadResults(endpoint)));
                _loaded = true;
            }
            finally
            {
                _loading.Release();
            }
        }
    }
}