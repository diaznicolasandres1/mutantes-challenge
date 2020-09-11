using Mutantes.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Infraestructura.Interfaces
{
   public interface IDnaAnalyzedRepository
    {
        public Task CreateAsync(DnaAnalyzed dnaAnalyzed);
    }
}
