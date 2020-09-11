using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Core.Interfaces.Dna
{
    public  interface IDnaSaverService
    {

        public  Task saveDnaResultAsync(string[] dna, bool isMutant);
    }
}
