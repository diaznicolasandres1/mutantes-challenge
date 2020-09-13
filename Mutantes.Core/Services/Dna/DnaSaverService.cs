using Mutantes.Core.Exceptions;
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
        IDnaAnalyzedRepository _dnaAnalyzedRepository;
        IStatsRepository _statsRepository;

        public DnaSaverService(IDnaAnalyzedRepository dnaAnalyzedRepository, IStatsRepository statsRepository)
        {
            _dnaAnalyzedRepository = dnaAnalyzedRepository;
            _statsRepository = statsRepository;

        }
        public async Task saveDnaResultAsync(string[] dna, bool isMutant)
        {
            if (dna == null) throw new NullDnaParameterException();

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
