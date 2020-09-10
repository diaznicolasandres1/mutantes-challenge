using System;
using System.Collections.Generic;
using System.Text;

namespace Mutantes.Core.Exceptions
{
    public class InvalidCharacterInListException : MutantsException
    {
        public InvalidCharacterInListException()
        {

        }

        public InvalidCharacterInListException(string message) : base(message)
        {

        }
    }
}
