using Microsoft.EntityFrameworkCore;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Infraestructura.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        MutantsDbContext _context;

        public StatsRepository(MutantsDbContext context)
        {
            _context = context;
        }
        public async Task<AnalysisStats> GetStats()
        {
            var stats = await _context.AnalysisStats.FirstOrDefaultAsync();
            return stats;
        }
    }
}
