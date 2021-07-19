using System;
using System.Collections.Generic;
using System.Linq;
using MatchAgregationServiceTests;
using Newtonsoft.Json;

namespace MatchAgregationService.Services
{
    internal class ResultParser : IResultParser
    {
        public IEnumerable<TeamResult> Parse(string resultString)
        {
            var parsedResult = JsonConvert.DeserializeObject<List<GameResult>>(resultString);
            return parsedResult!.SelectMany(r => new[]
            {
                new TeamResult
                {
                    Date = r.Date,
                    TeamName = r.HomeTeam,
                    Rival = r.AwayTeam,
                    Scored = r.HomeScore,
                    Received = r.AwayScore
                },
                new TeamResult
                {
                    Date = r.Date,
                    Rival = r.HomeTeam,
                    TeamName = r.AwayTeam,
                    Received = r.HomeScore,
                    Scored = r.AwayScore
                }
            });
        }

        // ReSharper disable once ClassNeverInstantiated.Local - DTO
        private record GameResult
        {
#pragma warning disable 649 //DTO
            public DateTime Date;
            public string HomeTeam;
            public string AwayTeam;
            public int HomeScore;
            public int AwayScore;
#pragma warning restore 649
        }
    }
}