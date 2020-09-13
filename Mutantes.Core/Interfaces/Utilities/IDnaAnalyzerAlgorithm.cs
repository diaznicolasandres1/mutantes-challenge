using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.Interfaces.Utilities
{
    public interface IDnaAnalyzerAlgorithm
    {

        public bool isMutant(string[] dna);
    }
}
