using MatchAgregationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchAgregationService.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class MatchAgregation : ControllerBase
    {
        public IActionResult Agregate()
        {
            return Ok(new SportResult
            {
                MostWin = new TeamData {Name = "Name1", Amount = 0.1},
                LessReceivedPerGame = new TeamData {Name = "Name1", Amount = 1.0},
                MostScoredPerGame = new TeamData {Name = "Name2", Amount = 12}
            });
        }
    }
}