using AutoFixture;
using FakeItEasy;
using MatchAgregationService.Services;
using NUnit.Framework;

namespace MatchAgregationServiceTests.Services
{
    public class TeamResultStorageTest: TestBase
    {
        private ITeamStatistic _teamStatistic;

        [SetUp]
        public void Setup()
        {
            _teamStatistic = A.Fake<ITeamStatistic>();
        }

        [Test]
        public void AddResult_WithValidResult_StoreResult()
        {
            TeamResult result = Fixture.Create<TeamResult>();

            var storage = CreateTeamResultStorage();
            
            storage.AddResult(result);

            A.CallTo(() => _teamStatistic.Update(result.TeamName, result.Scored, result.Received)).MustHaveHappenedOnceExactly();
        }

        private TeamResultStorage CreateTeamResultStorage()
        {
            return new TeamResultStorage(_teamStatistic);
        }
    }
}
