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
                throw new NullDnaParameterException("Empty string list, please insert a valid matriz (at least 4x4)");
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
                            throw new InvalidCharacterInListException("Invalid char in the matix, only permitted: G, T, A, C.");
                        }

                        matrizChar[fil, col] = rowToCharArr[col];


                    }
                }

            }
            catch (IndexOutOfRangeException)
            {

                throw new NonSquareMatrixException("NonSquare matrix error. Please insert a valid matriz (at least 4x4)");
            }


            return matrizChar;
        }

    }
}
