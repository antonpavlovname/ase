using AutoFixture;
using NUnit.Framework;

namespace MatchAgregationServiceTests
{
    public class TestBase
    {
        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void BaseSetup()
        {
            Fixture = new Fixture();
        }

    }
}