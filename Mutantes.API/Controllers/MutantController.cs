using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using StackExchange.Redis;
using Mutantes.API.Utilities;
using Mutantes.Core.DTOs;
using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Mutantes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutantController : Controller
    {
        IDnaAnalyzerService _dnaAnalyzerService;
        ICacheService _redisCache;



        public MutantController(IDnaAnalyzerService dnaAnalyzerService)
        {
            _dnaAnalyzerService = dnaAnalyzerService;
          
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DnaDto dnaRequest)
        {

            if (!ModelState.IsValid || dnaRequest.dna.Length < 4)
                return CustomBadRequest("Invalid DNA: Empty or invalid length, minimun matrix size is 4x4");

            try
            {             
                                
                var dnaEntitie = new DnaEntitie { Dna = dnaRequest.dna };
                bool isMutant = await _dnaAnalyzerService.IsMutantAsync(dnaEntitie);                 
             

                if (isMutant)
                {
                    return MutantFound();
                }

                return HumanFound();
            }

            catch (MutantsException exception)
            {

                return CustomBadRequest(exception.Message);
            }
            catch (Exception e)
            {

                return InternalServerError(e.Message);
            }

        }





      



        private BadRequestObjectResult CustomBadRequest(string message)
        {
           var response = new MessageReponse{
                status = 400,
                tittle =  "Bad Request.",
                message = message
            };
            return BadRequest(response);

        }


        private OkObjectResult MutantFound()
        {
            var response = new MessageReponse
            {
                status = 200,
                tittle = "Dna analisis result",
                message = "DNA corresponds to a mutant"
            };
            return Ok(response);
        }

        private ObjectResult HumanFound()
        {
            var response = new MessageReponse
            {
                status = 403,
                tittle = "Dna analisis result",
                message = "DNA corresponds to a human"
            };
            return StatusCode(403,response);
        }

        private ObjectResult InternalServerError(string message)
        {
            var response = new MessageReponse
            {
                status = 500,
                tittle = "INTERNAL ERROR ERROR",
                message = message
            };
            return StatusCode(500, response);
        }





    }
    
}
