using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Interfaces.Dna;
using Mutantes.Core.Interfaces.Utilities;
using Mutantes.Core.Services;
using Mutantes.Core.Utilities;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mutantes.UnitTests
{
    [TestClass]
    public class DnaAnalyzerServiceTest
    {
        readonly IMatrixUtilities _matrixUtilities;
        readonly IDnaAnalyzerAlgorithm _dnaAnalyzerAlgorithm;
        readonly  DnaAnalyzerService _dnaAnalizerService;


        readonly  Mock<ICacheRepository> cacheRepository = new Mock<ICacheRepository>();
        readonly  Mock<IDnaSaverService> dnaSaverService = new Mock<IDnaSaverService>();
        

        readonly DnaAnalyzed test = new DnaAnalyzed()
        {
            DateAnalyzed = DateTime.Now,
            Dna = "TESTDNA",
            IsMutant = true
        };



        public DnaAnalyzerServiceTest()
        {
            
            _matrixUtilities = new MatrixUtilities();
            _dnaAnalyzerAlgorithm = new DnaAnalyzerAlgorithm(_matrixUtilities);

            string[]  dna = DnaListGenerator.DnaHumanMatriz();

            dnaSaverService.Setup(x => x.saveDnaResultAsync(dna, false)).Returns(Task.CompletedTask);

            cacheRepository.Setup(x => x.CacheResponseAsync("key", new Random().NextDouble().ToString())).Returns(Task.CompletedTask);
            cacheRepository.Setup(x => x.GetCachedResponseAsync("key")).Returns(Task.FromResult(default(string)));

            _dnaAnalizerService = new DnaAnalyzerService(dnaSaverService.Object, cacheRepository.Object, _dnaAnalyzerAlgorithm);

       

        }


        [TestMethod]
        [ExpectedException(typeof(NonSquareMatrixException))]
        public async Task Test001TratarDeAnalizarUnaMatrizNoCuadradaLanzaNonSquareMatrixExceptionAsync()
        {
          

            
            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = DnaListGenerator.NonSquareMatrix()
            };
           await _dnaAnalizerService.IsMutantAsync(dnaEntitie);
        }



        [TestMethod]
        [ExpectedException(typeof(NullDnaParameterException))]
        public async Task Test002TratarDeAnalizarUnaMatrizNulaLanzaNullDnaParameterExceptionExcepcionAsync()
        {
            string[] dnaList = null;
            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = dnaList
            };
            await _dnaAnalizerService.IsMutantAsync(dnaEntitie);

        }

        [TestMethod]
        [ExpectedException(typeof(NullParameterException))]
        public async Task Test002TratarDeAnalizarDnaRecibiendoUnaEntidadNulaComoParametroLanzaParameterNullExceptionAsync()
        {

            DnaEntitie dnaEntitie = null;
            await _dnaAnalizerService.IsMutantAsync(dnaEntitie);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterInListException))]
        public async Task Test004MatrizConLetraInvalidaLanzaExcepcionAsync()
        {

            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = DnaListGenerator.InvalidCharMatrix()
            };

           await _dnaAnalizerService.IsMutantAsync(dnaEntitie);
        }


        [TestMethod]
        public async Task Test005AnalizarUnaMatrizConDatosMutanteDevuelveTrue()
        {

            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = DnaListGenerator.DnaMutantMatrix()
            };

            var esMutante =await  _dnaAnalizerService.IsMutantAsync(dnaEntitie);
            Assert.IsTrue(esMutante);

        }


        /*T-T-G-C-G-A
          C-A-G-T-G-A
          T-T-A-T-G-G
          A-G-T-A-G-G
          C-C-T-C-T-A
          T-C-A-C-T-G

         */
        [TestMethod]
        public async Task Test006AnalizarUnaMatrizConDatosHumanosDevuelveFals()
        {

            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = DnaListGenerator.DnaHumanMatriz()
            };
            var esMutante = await _dnaAnalizerService.IsMutantAsync(dnaEntitie);
            Assert.IsFalse(esMutante);

        }
    }





    }
