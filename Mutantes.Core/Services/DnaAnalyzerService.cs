using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mutantes.Core.Services
{
    public class DnaAnalyzerService : IDnaAnalyzerService
    {
        private int sequencesNeeded = 2;
        private int consecLettersNeeded = 4;
        private int matrixLenght;



        IMatrixUtilities _matrixUtilities;
        public DnaAnalyzerService(IMatrixUtilities matrixUtilities)
        {
            _matrixUtilities = matrixUtilities;
        }

        public bool isMutant(DnaEntitie dnaEntitie)
        {
            if (dnaEntitie == null ) throw new NullParameterException("Null paramater, please use a valid request.");
            return isMutant(dnaEntitie.Dna);

        }


         private bool isMutant(string[] dna)
         {                  
          
            var cantAdnsFound = 0;
            var matrix =  _matrixUtilities.getMatrixFromList(dna);
            if(matrix == null)
            {
                throw new NullDnaParameterException("Empty string list, please insert a valid matriz(at least 4x4)");
            }
            matrixLenght = dna.Length;

            for (int i = 0; i < dna.Length; i++)
            {
                for (int j = 0; j < dna.Length; j++)
                {
                    var currentChar = matrix[i, j];

                    if (lookForValidRepetitions(matrix, i, j, i, j, 0, currentChar))
                    {                      
                        cantAdnsFound++;
                        if (cantAdnsFound.Equals(sequencesNeeded)) return true;

                    }
                }

            }

            return false;

        }

        public bool lookForValidRepetitions(char[,] matriz, int oldRow, int oldCol, int actualRow, int actualCol, int lettersFound, char nextLetter)
        {
            //Al ser un metodo recursivo primero planteo los casos borde.

            if (lettersFound.Equals(consecLettersNeeded))
            {
                return true;
            }

            //Limites del tablero y que no sea la letra buscada
            if ( !isValidCoord(actualRow,actualCol) || !matriz[actualRow, actualCol].Equals(nextLetter)) return false;

            bool resultado = false;


            if (lettersFound > 0)
            {
                var sumaFila = actualRow - oldRow; //Calculo donde moverse para respetar la direccion previa.
                var sumaCol = actualCol - oldCol;

                resultado = lookForValidRepetitions(matriz, actualRow, actualCol, actualRow + sumaFila, actualCol + sumaCol, lettersFound + 1, nextLetter);

            }
            else
            {
                resultado = lookForValidRepetitions(matriz, oldRow, oldCol, actualRow - 1, actualCol + 1, 1, nextLetter) || //Diagonal superrior der.
                    lookForValidRepetitions(matriz, oldRow, oldCol, actualRow, actualCol + 1, 1, nextLetter) || //Derecha.
                    lookForValidRepetitions(matriz, oldRow, oldCol, actualRow + 1, actualCol + 1, 1, nextLetter) ||// Diagonal inferior der.
                    lookForValidRepetitions(matriz, oldRow, oldCol, actualRow + 1, actualCol, 1, nextLetter); //Inferior.
            }

            return resultado;



        }


        private bool isValidCoord(int filActual, int colActual )
        {
            return !(filActual < 0 || filActual >= matrixLenght || colActual < 0 || colActual >= matrixLenght);
        }

     

    
    }
}
