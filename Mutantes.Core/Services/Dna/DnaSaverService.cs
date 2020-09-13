using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Interfaces.Dna;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Core.Services.Dna
{
    public class DnaSaverService : IDnaSaverService
    {
        readonly IDnaAnalyzedRepository _dnaAnalyzedRepository;
        readonly IStatsRepository _statsRepository;
        readonly ICacheService _cacheService;

        public DnaSaverService(IDnaAnalyzedRepository dnaAnalyzedRepository, ICacheService cacheService, IStatsRepository statsRepository)
        {
            _dnaAnalyzedRepository = dnaAnalyzedRepository;
            _statsRepository = statsRepository;
            _cacheService = cacheService;

        }
        public async Task saveDnaResultAsync(string[] dna, bool isMutant)
        {
            if (dna == null)
            {
                throw new NullDnaParameterException();
            }

            var dnaString = string.Join(",", dna);
            DnaAnalyzed dnaAnalyzed = new DnaAnalyzed
            {
                IsMutant = isMutant,
                Dna = dnaString,
                DateAnalyzed = DateTime.Now

            };

            await _dnaAnalyzedRepository.CreateAsync(dnaAnalyzed);
            await _statsRepository.UpdateStatsAsync(dnaAnalyzed);
        }

    }
}
