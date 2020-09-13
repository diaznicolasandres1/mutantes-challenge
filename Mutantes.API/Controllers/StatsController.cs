using Microsoft.AspNetCore.Mvc;
using Mutantes.API.SwaggerExamples.Responses;
using Mutantes.API.SwaggerExamples.Responses.DnaResponses;
using Mutantes.Core.DTOs;
using Mutantes.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
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
        
        /// <summary>
        /// Returns all stats
        /// </summary>
       
        [HttpGet]
        [SwaggerResponseExample(200, typeof(StatsResponseExample))]
        [SwaggerResponse(200, "", typeof(StatsResponseExample))]
        public async Task<ActionResult> GetStats()
        {
            var stats = await _statsService.GetStats();
            StatsDto statsDto = new StatsDto
            {
                count_human_dna = stats.count_human_dna,
                count_mutant_dna = stats.count_mutant_dna,
                ratio = stats.ratio
            };
            return Ok(statsDto);
           
        }

    }
}
