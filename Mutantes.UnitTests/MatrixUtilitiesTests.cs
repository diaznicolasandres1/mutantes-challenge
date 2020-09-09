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
        public void Test002TratarDeConvetirUnaMatrizNulaRetornaNulo()
        {
            string[] dnaList = null;
            var convertedMatrix = utilites.getMatrixFromList(dnaList);
            Assert.AreEqual(null, convertedMatrix);

        }


        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterInListException))]
        public void Test003TratarDeConvetirUnaMatrizConCaracteresInvalidosLanzaNonSquareMatrixException()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCNCTG" };           
            utilites.getMatrixFromList(dnaList);
        }

        [TestMethod]
        public void Test004ConvertirUnaMatrizCuadradaYCaracteresValidosFuncionaCorrectamente()
        {
            string[] dnaList = { "ATG", "CAG", "TTA"};
            char[,] dnaMatrix= { { 'A', 'T', 'G' }, { 'C', 'A', 'G' }, { 'T', 'T', 'A' } };
            var dnaListConverted = utilites.getMatrixFromList(dnaList);
            CollectionAssert.Equals(dnaMatrix, dnaListConverted);

        }





    }
}
