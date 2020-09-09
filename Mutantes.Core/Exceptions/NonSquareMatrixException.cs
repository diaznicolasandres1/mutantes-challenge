using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.Exceptions
{
    public class NonSquareMatrixException : Exception
    {
        public NonSquareMatrixException()
        {

        }

        public NonSquareMatrixException(string message) : base(message)
        {

        }
    }
}
