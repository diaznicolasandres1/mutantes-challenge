using Mutantes.API.Utilities;
using Swashbuckle.AspNetCore.Filters;
using Mutantes.Core.DTOs;

namespace Mutantes.API.SwaggerExamples.Responses
{
    public class StatsResponseExample : IExamplesProvider<StatsDto>  
    {

   
        public StatsDto GetExamples()
        {
            return new StatsDto
            {
                count_mutant_dna = 110,
                count_human_dna = 17,
                ratio = 0.154
            };
        }
    }
}
