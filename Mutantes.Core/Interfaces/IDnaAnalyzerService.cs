using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Core.Interfaces
{
    public interface IDnaAnalyzerService
    {
        public  bool isMutant(string[] dna);
        
    }
}
