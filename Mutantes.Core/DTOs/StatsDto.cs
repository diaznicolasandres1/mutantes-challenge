using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.DTOs
{
    public class StatsDto
    {
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }

        public double ratio { get; set; }
    }
}
