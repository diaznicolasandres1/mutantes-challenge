using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Services;
using Mutantes.Core.Utilities;
using Newtonsoft.Json.Bson;
using System.Threading;

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
            string[] dna = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTGGGGGGGGg" };
            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = dna
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
        [ExpectedException(typeof(ParameterNullException))]
        public void Test002TratarDeAnalizarRecibiendoUnaEntidadNulaComoParametroLanzaParameterNullException()
        {
            
            DnaEntitie dnaEntitie = null;        
            _dnaAnalizerService.isMutant(dnaEntitie);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterInListException))]
        public void Test004MatrizConLetraBInvalidaLanzaExcepcion()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTBTGT", "AGAAGG", "CCCCTA", "TCACTG" };
            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = dnaList
            };

            _dnaAnalizerService.isMutant(dnaEntitie);
        }


        [TestMethod]
       
        public void Test005AnalizarUnaMatrizConDatosMutanteDevuelveTrue()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = dnaList
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
        public void Test006AnalizarUnaMatrizConDatosHumanosDevuelveFalse()
        {
            string[] dnaList = { "TTGCGA", "CAGTGA", "TTATGG", "AGTAGG", "CCTCTA", "TCACTG" };
            DnaEntitie dnaEntitie = new DnaEntitie
            {
                Dna = dnaList
            };
            var esMutante = _dnaAnalizerService.isMutant(dnaEntitie);
            Assert.IsFalse(esMutante);

        }










    }
}
