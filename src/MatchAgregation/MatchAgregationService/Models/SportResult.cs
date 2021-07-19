namespace MatchAgregationService.Models
{
    public record SportResult
    {
        public TeamData MostWin { get; init; }
        public TeamData MostScoredPerGame { get; init; }
        public TeamData LessReceivedPerGame { get; init; }
    }
}