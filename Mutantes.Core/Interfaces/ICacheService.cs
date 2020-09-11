using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Core.Interfaces
{
   public  interface ICacheService
    {

        public  Task CacheResponseAsync(string cacheKey, string value);

       public  Task<string> GetCachedResponseAsync(string cacheKey);

    }
}
