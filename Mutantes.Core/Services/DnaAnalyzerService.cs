using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mutantes.Core.Services
{
    public class DnaAnalyzerService : IDnaAnalyzerService
    {
        private int sequencesNeeded = 2;
        private int consecLettersNeeded = 4;
        private int matrixLenght;

        public bool isMutant(string[] dna)
        {
            throw new NotImplementedException();
        }
    }
}
