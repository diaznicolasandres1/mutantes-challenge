using Microsoft.EntityFrameworkCore;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;


namespace Mutantes.Infraestructura.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        readonly MutantsDbContext _context;
        ICacheRepository _cacheService;

        public StatsRepository(MutantsDbContext context, ICacheRepository cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<AnalysisStats> GetStats()
        {

            AnalysisStats stats = await GetStatsFromCacheAsync();

            if (stats == null)
            {
                stats = await _context.AnalysisStats.FirstOrDefaultAsync();
            }


            if(stats == null)
            {
                stats = new AnalysisStats
                {
                    HumansFound = 0,
                    MutantsFound = 0,

                };
            }
            return stats;
         

      
    }

        public async Task UpdateStatsAsync(DnaAnalyzed dnaAnalyzed)
        {
            
            AnalysisStats stats = await GetStatsFromCacheAsync();

            if(stats == null)
            {
                stats = await GetStats().ConfigureAwait(false);
            }       

            

            if (dnaAnalyzed.IsMutant)
            {
                stats.MutantsFound++;
            }
            else
            {
                stats.HumansFound++;
            }

            var statsSerialized = JsonSerializer.Serialize(stats);

            await _cacheService.CacheResponseAsync("stats", statsSerialized);

            _context.Update(stats);

            await _context.SaveChangesAsync();
        }

        private  async Task<AnalysisStats> GetStatsFromCacheAsync()
        {
            AnalysisStats analizysSerialized = null;

            var statsCache = await _cacheService.GetCachedResponseAsync("stats");
           
            if (statsCache != null)
            {
                analizysSerialized = JsonSerializer.Deserialize<AnalysisStats>(statsCache);
            }
            return analizysSerialized;
        }
    }
}
