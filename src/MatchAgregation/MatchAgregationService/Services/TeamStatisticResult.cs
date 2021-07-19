namespace MatchAgregationService.Services
{
    public record TeamStatisticResult
    {
        public string Team { get; init; }
        public double ScoredPerGame { get; init; }
        public double ReceivedPerGame { get; init; }

        public double Wins { get; init; }
    }
}