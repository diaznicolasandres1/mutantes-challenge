using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.Exceptions
{
   public  class ParameterNullException : Exception

   {
        public ParameterNullException()
        {

        }
        public ParameterNullException(string message) :base (message)
        {

        }
    }
}
