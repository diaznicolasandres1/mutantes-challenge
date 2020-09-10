using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.Exceptions
{
   public  class NullParameterException : MutantsException

   {
        public NullParameterException()
        {

        }
        public NullParameterException(string message) :base (message)
        {

        }
    }
}
