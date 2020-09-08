using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mutantes.Core.Interfaces;

namespace Mutantes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController :ControllerBase
    {
        IStatsRepository _statsRepository;
        public StatsController(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var s =await  _statsRepository.GetStats();

            return Ok(s);
        }
    }
}
