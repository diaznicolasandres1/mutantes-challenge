using Mutantes.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Infraestructura.Interfaces
{
    public interface IStatsRepository
    {
        public  Task<AnalysisStats> GetStats();
    }
}
