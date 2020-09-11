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
            //Contemplar nulo y lo vamos subiendo, evitar exception por perfomance
            var stats = await _statsRepository.GetStats();
            var statsEntitie = new StatsEntitie
            {
                count_human_dna = stats.HumansFound,
                count_mutant_dna = stats.MutantsFound
            };

            return statsEntitie;
        }
    }
}
