using Microsoft.EntityFrameworkCore;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            /*var stats = await _context.AnalysisStats.FirstOrDefaultAsync();
           

             if(stats == null)
             {
                 stats = new AnalysisStats
                 {
                     HumansFound = 0,
                     MutantsFound = 0,

                 };
             }
             return stats;
            */

            var humanos = 0;
            var mutantes = 0;
            var list = _context.DnaAnalyzed.ToList();

            foreach (var lis in list)
            {
                if (lis.IsMutant)
                {
                    humanos++;
                }
                else
                {
                    mutantes++;
                }
            }
          


            var stats = new AnalysisStats
            {
                HumansFound = humanos,
                MutantsFound = mutantes,

            };
           await Task.Delay(1);
            return stats;
      
    }

        public async Task UpdateStatsAsync(DnaAnalyzed dnaAnalyzed)
        {
            if(dnaAnalyzed == null)
            {
                throw new Exception("Null DnaAnalyzed Paramater");
            }
            var stats = await GetStats();

            if (dnaAnalyzed.IsMutant)
            {
                stats.MutantsFound++;
            }
            else
            {
                stats.HumansFound++;
            }
             _context.Update(stats);

            await _context.SaveChangesAsync();
        }
    }
}
