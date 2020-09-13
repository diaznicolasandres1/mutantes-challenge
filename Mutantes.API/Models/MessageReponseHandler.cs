using Mutantes.API.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mutantes.API.Models
{
    public static class MessageReponseHandler
    {

        public static MessageReponse CustomBadRequest(string message)
        {
            return new MessageReponse
            {
                status = 400,
                tittle = "Bad Request.",
                message = message
            };
        }

        public static MessageReponse MutantFound()
        {
            return new MessageReponse
            {
                status = 200,
                tittle = "Dna analisis result",
                message = "DNA corresponds to a mutant"
            };
        }

        public static MessageReponse HumanFound()
        {
            return new MessageReponse
            {
                status = 403,
                tittle = "Dna analisis result",
                message = "DNA corresponds to a human"
            };
        }

        public static MessageReponse InternalServerError(string message)
        {
            return new MessageReponse
            {
                status = 500,
                tittle = "Internal server errro",
                message = message
            };
        }


        public static MessageReponse UnsupportedMediaType(string message)
        {
            return new MessageReponse
            {
                status = 415,
                tittle = "UnsupportedMediaType",
                message = message
            };
        }
    }
}
