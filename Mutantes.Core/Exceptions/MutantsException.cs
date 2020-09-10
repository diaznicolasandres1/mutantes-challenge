using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mutantes.Core.Exceptions
{
    public class MutantsException : Exception
    {
        public MutantsException()
        {

        }
        public MutantsException(string message) : base(message)
        {

        }
    }
}
