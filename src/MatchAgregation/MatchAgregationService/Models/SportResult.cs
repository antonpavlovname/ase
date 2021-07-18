namespace MatchAgregationService.Models
{
    public class SportResult
    {
        public TeamData MostWin { get; init; }
        public TeamData MostScoredPerGame { get; init; }
        public TeamData LessReceivedPerGame { get; init; }
    }
}