using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mutantes.Core.DTOs;
using Mutantes.Core.Entities;
using Mutantes.Core.Interfaces;

namespace Mutantes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutantController : Controller
    {
        IDnaAnalyzerService _dnaAnalyzerService;
        public MutantController(IDnaAnalyzerService dnaAnalyzerService)
        {
            _dnaAnalyzerService = dnaAnalyzerService;
        }
        public ActionResult Index(DnaDto dnaRequest)
        {

            var dnaEntitie = new DnaEntitie
            {
                Dna = dnaRequest.Dna
            };


            var isMutant = _dnaAnalyzerService.isMutant(dnaEntitie);
            if (isMutant)
            {
                return Ok("Es mutanteee");
            }
            return BadRequest("Es human");
        }
    }
}
