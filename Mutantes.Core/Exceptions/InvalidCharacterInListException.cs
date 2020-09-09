using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.Exceptions
{
    public class InvalidCharacterInListException : Exception
    {
        public InvalidCharacterInListException()
        {

        }

        public InvalidCharacterInListException(string message) : base(message)
        {

        }
    }
}
