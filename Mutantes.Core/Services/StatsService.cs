using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mutantes.Core.Entities;
using Mutantes.Core.Interfaces;
using Mutantes.Infraestructura.Interfaces;

namespace Mutantes.Core.Services
{
    public class StatsService : IStatsService
    {
        IStatsRepository _statsRepository;
        public StatsService(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public async Task<StatsEntitie> GetStats()
        {
           
            var stats = await _statsRepository.GetStats();
            var statsEntitie = new StatsEntitie
            {
                count_human_dna = stats.HumansFound,
                count_mutant_dna = stats.MutantsFound,
                ratio = stats.HumansFound != 0 ? (double)decimal.Divide(stats.MutantsFound, stats.HumansFound) : 0.0
                
            };

            return statsEntitie;
        }


    }
}
