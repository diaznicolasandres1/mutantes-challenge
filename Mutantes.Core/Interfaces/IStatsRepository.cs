using Mutantes.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Core.Interfaces
{
    public interface IStatsRepository
    {
        public  Task<Stats> GetStats();
    }
}
