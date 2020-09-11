using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mutantes.Infraestructura.Data
{
    public partial class AnalysisStats
    {
        public int Id { get; set; }
        public int MutantsFound { get; set; }
        public int HumansFound { get; set; }
        public DateTime LastModification { get; set; }
    }
}
