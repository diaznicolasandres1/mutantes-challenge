using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Mutantes.Core.Utilities
{
    public static class DnaListGenerator
    {       

        public static string[] NonSquareMatrix()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTGGGGGGGG" };
            return dnaList;
        }

        public static string[] InvalidCharMatrix()
        {
            string[] dnaList = { "ATGCGA", "CBGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCNCTG" };
            return dnaList;
        }

        public static string[] ValidCharMatrix()
        {
            string[] dnaList =  { "ATGA", "CAGA", "TTAA", "AATA"};
            return dnaList;
        }

        public static string[] DnaMutantMatrix()
        {
            string[] dnaList = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            return dnaList;
        }

        public static string[] DnaHumanMatriz()
        {
            string[] dnaList = { "TTGCGA", "CAGTGA", "TTATGG", "AGTAGG", "CCTCTA", "TCACTG" };
            return dnaList;
        }

        public static string[] EmptyMatrix()
        {
            string[] dnaList = {  };
            return dnaList;
        }

        
    }
}
