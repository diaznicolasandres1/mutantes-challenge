using Mutantes.API.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace Mutantes.API.SwaggerExamples.Responses.DnaResponses
{
    public class OkMutantExample : IExamplesProvider<MessageReponse>
    {
        public MessageReponse GetExamples()
        {
            return new MessageReponse
            {
                status = 200,
                tittle = "Dna analisis result",
                message = "DNA corresponds to a mutant"
            };

        }
    }
}
