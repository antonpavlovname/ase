using System;

namespace MatchAgregationServiceTests
{
    public record TeamResult
    {
        public string TeamName { get; init; }
        public string Rival { get; init; }
        public DateTime Date { get; init; }
        public int Scored { get; init; }
        public int Received { get; init; }
    }
}