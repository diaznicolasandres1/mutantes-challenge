using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Services;
using Mutantes.Core.Utilities;

namespace Mutantes.UnitTests
{
    [TestClass]
    public class DnaAnalyzerServiceTest
    {
        MatrixUtilities _matrixUtilities;
        DnaAnalyzerService _dnaAnalizerService;
        public DnaAnalyzerServiceTest()
        {
            _matrixUtilities = new MatrixUtilities();
            _dnaAnalizerService = new DnaAnalyzerService(_matrixUtilities);

        }


        [TestMethod]
        [ExpectedException(typeof(NonSquareMatrixException))]
        public void Test001TratarDeAnalizarUnaMatrizNoCuadradaLanzaNonSquareMatrixException()
        {
            //string[] dna = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTGGGGGGGG" };
            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = DnaListGenerator.NonSquareMatrix()
            };
            _dnaAnalizerService.isMutant(dnaEntitie);
        }



        [TestMethod]
        [ExpectedException(typeof(NullDnaParameterException))]
        public void Test002TratarDeAnalizarUnaMatrizNulaLanzaNullDnaParameterExceptionExcepcion()
        {
            string[] dnaList = null;
            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = dnaList
            };
            _dnaAnalizerService.isMutant(dnaEntitie);

        }

        [TestMethod]
        [ExpectedException(typeof(NullParameterException))]
        public void Test002TratarDeAnalizarDnaRecibiendoUnaEntidadNulaComoParametroLanzaParameterNullException()
        {

            DnaEntitie dnaEntitie = null;
            _dnaAnalizerService.isMutant(dnaEntitie);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterInListException))]
        public void Test004MatrizConLetraInvalidaLanzaExcepcion()
        {

            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = DnaListGenerator.InvalidCharMatrix()
            };

            _dnaAnalizerService.isMutant(dnaEntitie);
        }


        [TestMethod]
        public void Test005AnalizarUnaMatrizConDatosMutanteDevuelveTrue()
        {

            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = DnaListGenerator.DnaMutantMatrix()
            };

            var esMutante = _dnaAnalizerService.isMutant(dnaEntitie);
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
        public void Test006AnalizarUnaMatrizConDatosHumanosDevuelveFalse()
        {

            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = DnaListGenerator.DnaHumanMatriz()
            };
            var esMutante = _dnaAnalizerService.isMutant(dnaEntitie);
            Assert.IsFalse(esMutante);

        }
    }





    }
