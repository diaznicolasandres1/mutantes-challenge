using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTGGGGGGGGg" };
            _dnaAnalizerService.isMutant(dnaList);
        }

       

        [TestMethod]
        [ExpectedException(typeof(NullDnaParameterException))]
        public void Test002TratarDeAnalizarUnaMatrizNulaLanzaExcepcion()
        {
            string[] dnaList = null;
            _dnaAnalizerService.isMutant(dnaList);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterInListException))]
        public void Test003MatrizConLetraBInvalidaLanzaExcepcion()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTBTGT", "AGAAGG", "CCCCTA", "TCACTG" };
            _dnaAnalizerService.isMutant(dnaList);
        }


        [TestMethod]
       
        public void Test004AnalizarUnaMatrizConDatosMutanteDevuelveTrue()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            var esMutante = _dnaAnalizerService.isMutant(dnaList);
            Assert.IsTrue(esMutante);

        }



        /*T-T-G-C-G-A
          C-A-G-T-G-A
          T-T-A-T-G-G
          A-G-T-A-G-G
          C-C-T-C-T-A
          T-C-A-C-T-G

         */
        public void Test003AnalizarUnaMatrizConDatosHumanosDevuelveFalse()
        {
            string[] dnaList = { "TTGCGA", "CAGTGA", "TTATGG", "AGTAGG", "CCTCTA", "TCACTG" };
            var esMutante = _dnaAnalizerService.isMutant(dnaList);
            Assert.IsFalse(esMutante);

        }










    }
}
