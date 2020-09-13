using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.UnitTests
{
    [TestClass]
    public class MatrixUtilitiesTests
    {

        readonly MatrixUtilities utilites = new MatrixUtilities();

        [TestMethod]
        [ExpectedException(typeof(NonSquareMatrixException))]
        public void Test001TratarDeConvetirUnaMatrizNoCuadradaLanzaNonSquareMatrixException()
        {
            string[] dnaList = DnaListGenerator.NonSquareMatrix();
            utilites.getMatrixFromList(dnaList);

        }


        [TestMethod]
        public void Test002TratarDeConvetirUnaMatrizNulaRetornaNull()
        {
            var nullResponse = utilites.getMatrixFromList(null);

            Assert.IsNull(nullResponse);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterInListException))]
        public void Test003TratarDeConvertirUnaListaConCaracterInvalidoLanzaInvalidCharacterInListException()
        {
            string[] dnaList = DnaListGenerator.InvalidCharMatrix();
            utilites.getMatrixFromList(dnaList);
        }



        [TestMethod]
        public void Test005ConvertirUnaMatrizCuadradaYCaracteresValidosFuncionaCorrectamente()
        {
            string[] dnaList = DnaListGenerator.ValidCharMatrix();
            char[,] dnaMatrix = { { 'A', 'T', 'G', 'A' }, { 'C', 'A', 'G', 'A' }, { 'T', 'T', 'A', 'A' }, { 'A', 'A', 'T', 'A' } };
            var dnaListConverted = utilites.getMatrixFromList(dnaList);
            CollectionAssert.Equals(dnaMatrix, dnaListConverted);

        }





    }
}
