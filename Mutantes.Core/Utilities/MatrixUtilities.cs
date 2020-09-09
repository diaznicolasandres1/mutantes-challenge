using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.Utilities
{
    public  class  MatrixUtilities : IMatrixUtilities
    {


        List<char> posibleLetters = new List<char>
        {
            'A','T','C','G'
        };
        
        public char[,] getMatrixFromList(string[] dna)
        {
            if (dna == null)
            {
                return null;
            }

            var matrixLength = dna.Length;
            char[,] matrizChar = new char[matrixLength, matrixLength];

            try
            {
                for (int fil = 0; fil < matrixLength; fil++)
                {
                    char[] rowToCharArr = dna[fil].ToCharArray();

                    for (int col = 0; col < rowToCharArr.Length; col++)
                    {
                        char currentChar = rowToCharArr[col];

                        if (!posibleLetters.Contains(currentChar))
                        {
                            throw new InvalidCharacterInListException();
                        }

                        matrizChar[fil, col] = rowToCharArr[col];


                    }
                }

            }
            catch (IndexOutOfRangeException)
            {

                throw new NonSquareMatrixException();
            }


            return matrizChar;
        }

    }
}
