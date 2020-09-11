using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Services;
using Mutantes.Core.Utilities;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using Mutantes.Infraestructura.Repositories;
using System;
using System.Threading.Tasks;

namespace Mutantes.UnitTests
{
    [TestClass]
    public class DnaAnalyzerServiceTest
    {
        MatrixUtilities _matrixUtilities;
        DnaAnalyzerService _dnaAnalizerService;

        
        Mock<IDnaAnalyzedRepository> dnaAnalyzedRepository = new Mock<IDnaAnalyzedRepository>();
        Mock<IStatsRepository> statsRepository = new Mock<IStatsRepository>();

        DnaAnalyzed test = new DnaAnalyzed()
        {
            DateAnalyzed = DateTime.Now,
            Dna = "TESTDNA",
            IsMutant = true
        };



        public DnaAnalyzerServiceTest()
        {
            
            _matrixUtilities = new MatrixUtilities();

            statsRepository.Setup(x => x.UpdateStatsAsync(test)).Returns(Task.CompletedTask);

            dnaAnalyzedRepository.Setup(x => x.CreateAsync(test)).Returns(Task.CompletedTask);

            var asd = statsRepository.Object;
            var xd = dnaAnalyzedRepository.Object;

            _dnaAnalizerService = new DnaAnalyzerService(_matrixUtilities, xd, asd);

       

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
