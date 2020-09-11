using System;
using System.Collections.Generic;

namespace Mutantes.Infraestructura.Data
{
    public partial class DnaAnalyzed
    {
        public int Id { get; set; }
        public string Dna { get; set; }
        public bool IsMutant { get; set; }
        public DateTime DateAnalyzed { get; set; }
    }
}
