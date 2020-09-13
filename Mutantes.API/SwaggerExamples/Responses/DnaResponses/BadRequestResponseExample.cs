using Mutantes.API.Models;
using Mutantes.API.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mutantes.API.SwaggerExamples.Responses.DnaResponses
{
    public class BadRequestResponseExample : IExamplesProvider<MessageReponse>
    {
        public MessageReponse GetExamples()
        {
            var response = MessageReponseHandler.CustomBadRequest("Invalid DNA: Empty or invalid length, minimun matrix size is 4x4");
            return response;
        }
    }
}
