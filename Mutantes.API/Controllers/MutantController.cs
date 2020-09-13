using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mutantes.API.Models;
using Mutantes.API.SwaggerExamples.Responses.DnaResponses;
using Mutantes.API.Utilities;
using Mutantes.Core.DTOs;
using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mutantes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutantController : Controller
    {
        readonly IDnaAnalyzerService _dnaAnalyzerService;
        



        public MutantController(IDnaAnalyzerService dnaAnalyzerService)
        {
            _dnaAnalyzerService = dnaAnalyzerService;

        }


     
        [HttpPost]      
        //Annotations & filters para documentacion swagger, pongo los mas importantes
        [SwaggerResponseExample(200, typeof(OkMutantExample))][SwaggerResponse(200, "", typeof(OkMutantExample))]
        [SwaggerResponseExample(403, typeof(HumanExample))][SwaggerResponse(403, "", typeof(HumanExample))]
        [SwaggerResponseExample(400, typeof(BadRequestResponseExample))][SwaggerResponse(400, "", typeof(BadRequestResponseExample))]

        public async Task<IActionResult> Post([FromBody] DnaDto dnaRequest)
        {
            if (dnaRequest.dna == null)
            {
                return UnsupportedMediaType("Null DNA");
            }
            
           if (!ModelState.IsValid || dnaRequest.dna.Length < 4)
           {       
                return CustomBadRequest("Invalid DNA: Empty or invalid length, minimun matrix size is 4x4");
           }
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
            var response = MessageReponseHandler.CustomBadRequest(message);
            return BadRequest(response);

        }


        private OkObjectResult MutantFound()
        {
            var response = MessageReponseHandler.MutantFound();
            return Ok(response);
        }

        private ObjectResult HumanFound()
        {
            var response = MessageReponseHandler.HumanFound();
            return StatusCode(403,response);
        }

        private ObjectResult InternalServerError(string message)
        {
            var response = MessageReponseHandler.InternalServerError(message);
            return StatusCode(500, response);
        }

        private ObjectResult UnsupportedMediaType(string message)
        {
            var response = MessageReponseHandler.UnsupportedMediaType(message);

            return StatusCode(415, response);
        }




    }
    
}
