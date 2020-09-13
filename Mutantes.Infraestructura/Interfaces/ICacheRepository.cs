using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Infraestructura.Interfaces
{
   public  interface ICacheRepository
    {

        public  Task CacheResponseAsync(string cacheKey, string value);

       public  Task<string> GetCachedResponseAsync(string cacheKey);

    }
}
