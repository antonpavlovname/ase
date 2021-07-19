using System.Linq;
using MatchAgregationService.Services;
using NUnit.Framework;

namespace MatchAgregationServiceTests.Services
{
    public class ResultParserTest
    {
        [Test]
        public void Parse()
        {
            var parser = new ResultParser();
            var result = parser.Parse("[{\"date\":\"2000-01-04\",\"homeTeam\":\"Egypt\",\"awayTeam\":\"Togo\",\"homeScore\":2,\"awayScore\":1,\"tournament\":\"Friendly\",\"city\":\"Aswan\",\"country\":\"Egypt\"},{\"date\":\"2000-01-07\",\"homeTeam\":\"Tunisia\",\"awayTeam\":\"Togo\",\"homeScore\":7,\"awayScore\":0,\"tournament\":\"Friendly\",\"city\":\"Tunis\",\"country\":\"Tunisia\"}]");
            Assert.That(result.Count(), Is.EqualTo(4));
        }
    }
}
