using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.Entities
{
    public class StatsEntitie
    {
        public StatsEntitie()
        {
            ratio = count_human_dna != 0 ? count_mutant_dna / count_human_dna : 0;
        }
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }

        public double ratio { get; set; } 

    }
}
