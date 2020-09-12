using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Interfaces.Dna;
using Mutantes.Core.Services;
using Mutantes.Core.Utilities;
using Mutantes.Infraestructura.Data;
using System;
using System.Threading.Tasks;

namespace Mutantes.UnitTests
{
    [TestClass]
    public class DnaAnalyzerServiceTest
    {
        MatrixUtilities _matrixUtilities;
        DnaAnalyzerService _dnaAnalizerService;


        //Mock<IDnaAnalyzedRepository> dnaAnalyzedRepository = new Mock<IDnaAnalyzedRepository>();
        //Mock<IStatsRepository> statsRepository = new Mock<IStatsRepository>();

        Mock<ICacheService> cacheService = new Mock<ICacheService>();
        Mock<IDnaSaverService> dnaSaverService = new Mock<IDnaSaverService>();

        DnaAnalyzed test = new DnaAnalyzed()
        {
            DateAnalyzed = DateTime.Now,
            Dna = "TESTDNA",
            IsMutant = true
        };



        public DnaAnalyzerServiceTest()
        {
            
            _matrixUtilities = new MatrixUtilities();
           
            string[]  dna = DnaListGenerator.DnaHumanMatriz();

            dnaSaverService.Setup(x => x.saveDnaResultAsync(dna, false)).Returns(Task.CompletedTask);

            cacheService.Setup(x => x.CacheResponseAsync("key", new Random().NextDouble().ToString())).Returns(Task.CompletedTask);
            cacheService.Setup(x => x.GetCachedResponseAsync("key")).Returns(Task.FromResult(default(string)));

            _dnaAnalizerService = new DnaAnalyzerService(_matrixUtilities, dnaSaverService.Object, cacheService.Object);

       

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
        public async System.Threading.Tasks.Task Test002TratarDeAnalizarUnaMatrizNulaLanzaNullDnaParameterExceptionExcepcionAsync()
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
        public async Task Test005AnalizarUnaMatrizConDatosMutanteDevuelveTrueAsync()
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
        public async Task Test006AnalizarUnaMatrizConDatosHumanosDevuelveFalseAsync()
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
