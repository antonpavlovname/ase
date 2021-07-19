using System.Threading.Tasks;

namespace MatchAgregationService.Services
{
    public interface IMatchesResultLoader
    {
        Task LoadMatches();
    }
}