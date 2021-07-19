using System.Threading.Tasks;

namespace MatchAgregationService.Services
{
    public interface IMatchResultClient
    {
        Task LoadResults(string parameterString);
    }
}