using Mutantes.Core.DTOs;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mutantes.API.SwaggerExamples.Request
{
    public class AnalyzeDnaExample : IExamplesProvider<DnaDto>
    {
        public DnaDto GetExamples()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            return new DnaDto
            {
                dna = dnaList
            };
        }
    }
}
