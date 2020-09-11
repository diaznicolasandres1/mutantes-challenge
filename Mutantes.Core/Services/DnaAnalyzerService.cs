﻿using Mutantes.Core.Entities;
using Mutantes.Core.Exceptions;
using Mutantes.Core.Interfaces;
using Mutantes.Core.Utilities;
using Mutantes.Infraestructura.Data;
using Mutantes.Infraestructura.Interfaces;
using Mutantes.Infraestructura.Repositories;
using System;using System.Collections.Generic;

using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Core.Services
{
    public class DnaAnalyzerService : IDnaAnalyzerService
    {
        private int sequencesNeeded = 2;
        private int consecLettersNeeded = 4;
        private int matrixLenght;

        IDnaAnalyzedRepository _dnaAnalyzedRepository;
        IStatsRepository _statsRepository;

        



        IMatrixUtilities _matrixUtilities;
        private MatrixUtilities matrixUtilities;
        private IDnaAnalyzedRepository dnaAnalyzedRepository;
       

        public DnaAnalyzerService(IMatrixUtilities matrixUtilities, IDnaAnalyzedRepository analyzedRepository, IStatsRepository statsRepository)
        {
            _matrixUtilities = matrixUtilities;
            _dnaAnalyzedRepository = analyzedRepository;
            _statsRepository = statsRepository;
        }

   

        public async Task<bool> IsMutantAsync(DnaEntitie dnaEntitie)
        {
            if (dnaEntitie == null ) throw new NullParameterException("Null paramater, please use a valid request.");
            string[] dna = dnaEntitie.Dna;
            bool isMutantResult = isMutant(dna);

            try
            {
                await saveDnaResultAsync(dna, isMutantResult);
                
            }  
            catch (Exception e)
            {
                throw new ErrorSavingResultException("An unexpected error occurred, please try again");


            }
            return isMutantResult;


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

        private async Task saveDnaResultAsync(string[] dna,bool isMutant)
        {
            var dnaString = string.Join(",", dna);
            DnaAnalyzed dnaAnalyzed = new DnaAnalyzed
            {
                IsMutant = isMutant,
                Dna = dnaString,
                DateAnalyzed = DateTime.Now

            };

            await _dnaAnalyzedRepository.CreateAsync(dnaAnalyzed);
            await _statsRepository.UpdateStatsAsync(dnaAnalyzed);

        }





    }
}
