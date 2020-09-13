using Mutantes.API.Models;
using Mutantes.API.Utilities;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mutantes.API.SwaggerExamples.Responses.DnaResponses
{
    public class HumanExample : IExamplesProvider<MessageReponse>
    {
        public MessageReponse GetExamples()
        {
            return MessageReponseHandler.HumanFound();
        }
    }
}
