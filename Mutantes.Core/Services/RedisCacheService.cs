
using Microsoft.Extensions.Caching.Distributed;
using Mutantes.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutantes.Core.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task CacheResponseAsync(string cacheKey, string value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(cacheKey, value);
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(cacheKey);
        }
    }
}
