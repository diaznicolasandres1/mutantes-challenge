using System;
using System.Collections.Generic;

namespace Mutantes.Infraestructura.Data
{
    public partial class AnalysisStats
    {
        public int MutantsFound { get; set; }
        public int HumansFound { get; set; }
        public DateTime LastModification { get; set; }
    }
}
