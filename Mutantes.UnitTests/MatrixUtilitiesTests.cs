using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Utilities;

namespace Mutantes.UnitTests
{
    [TestClass]
    public class MatrixUtilitiesTests
    {

        MatrixUtilities utilites = new MatrixUtilities();

        [TestMethod]
        [ExpectedException(typeof(NonSquareMatrixException))]
        public void Test001TratarDeConvetirUnaMatrizNoCuadradaLanzaNonSquareMatrixException()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTGGGGG" };           
            utilites.getMatrixFromList(dnaList);

        }
        [TestMethod]   
        [ExpectedException(typeof(NullDnaParameterException))]
        public void Test002TratarDeConvetirUnaMatrizNulaRetornaExcepcion()
        {
            string[] dnaList = null;
            var convertedMatrix = utilites.getMatrixFromList(dnaList);
            

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterInListException))]
        public void Test003TratarDeConvertirUnaListaConCaracterInvalidoLanzaInvalidCharacterInListException()
        {
            string[] dnaList = { "ATGCGA", "CBGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCNCTG" };
            utilites.getMatrixFromList(dnaList);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterInListException))]
        public void Test004TratarDeConvetirUnaMatrizConCaracteresInvalidosLanzaNonSquareMatrixException()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCNCTG" };           
            utilites.getMatrixFromList(dnaList);
        }

        [TestMethod]
        public void Test005ConvertirUnaMatrizCuadradaYCaracteresValidosFuncionaCorrectamente()
        {
            string[] dnaList = { "ATG", "CAG", "TTA"};
            char[,] dnaMatrix= { { 'A', 'T', 'G' }, { 'C', 'A', 'G' }, { 'T', 'T', 'A' } };
            var dnaListConverted = utilites.getMatrixFromList(dnaList);
            CollectionAssert.Equals(dnaMatrix, dnaListConverted);

        }





    }
}
