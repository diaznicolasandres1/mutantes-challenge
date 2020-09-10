using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.Exceptions
{
    public class NullDnaParameterException : MutantsException
    {
        public NullDnaParameterException()
        {

        }

        public NullDnaParameterException(string message) : base(message)
        {

        }

    }
}
