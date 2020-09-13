using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Caching.Memory;
using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Interfaces.Dna;
using Mutantes.Core.Interfaces.Utilities;
using Mutantes.Core.Utilities;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using Mutantes.Infraestructura.Repositories;
using System;using System.Collections.Generic;

using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Core.Services
{
    public class DnaAnalyzerService : IDnaAnalyzerService
    {




        readonly IDnaAnalyzerAlgorithm _analyzerAlgorithm;
        readonly IDnaSaverService _dnaSaverService;
        readonly ICacheService _cacheService;


        public DnaAnalyzerService( IDnaSaverService dnaSaverService, ICacheService cacheService, IDnaAnalyzerAlgorithm analyzerAlgorithm)
        {           
            _dnaSaverService = dnaSaverService;
            _cacheService = cacheService;
            _analyzerAlgorithm = analyzerAlgorithm;
        }

   

        public async Task<bool> IsMutantAsync(DnaEntitie dnaEntitie)
        {
            if (dnaEntitie == null)
            {
                throw new NullParameterException("Null paramater, please use a valid request.");
            }
            string[] dna = dnaEntitie.Dna;

            bool isMutantResult = false;

            if (dna == null)
            {
                throw new NullDnaParameterException();
            }

            string dnaString = string.Join(",",dna);

            //Chequeo cache por su key: dnaString.
            var resultCache = await _cacheService.GetCachedResponseAsync(dnaString);
            if(resultCache != null)
            {               
                    isMutantResult = Convert.ToBoolean(resultCache);
            }
            else
            {
                isMutantResult =  _analyzerAlgorithm.isMutant(dna);
                await _cacheService.CacheResponseAsync(dnaString, isMutantResult.ToString());
            }

            try
            {
                await _dnaSaverService.saveDnaResultAsync(dna, isMutantResult);
                
            }  
            catch (Exception e)
            {
                throw new ErrorSavingResultException("An unexpected error occurred, please try again");


            }

            return isMutantResult;


        }


      
      




    }
}
