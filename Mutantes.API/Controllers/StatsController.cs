using Microsoft.AspNetCore.Mvc;
using Mutantes.Core.Interfaces;
using System.Threading.Tasks;

namespace Mutantes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        readonly IStatsService _statsService;
        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetStats()
        {
            var stats = await _statsService.GetStats();
            return Ok(stats);
           
        }

    }
}
