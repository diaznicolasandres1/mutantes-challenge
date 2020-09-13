using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mutantes.Core.Exceptions
{
    public class ErrorSavingResultException : Exception
    {

        public ErrorSavingResultException()
        {

        }

        public ErrorSavingResultException(string message) : base(message)
        {

        }
    }
}
