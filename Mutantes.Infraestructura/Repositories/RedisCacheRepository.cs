using Mutantes.Infraestructura.Interfaces;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Mutantes.Infraestructura.Repositories
{
    public class RedisCacheRepository : ICacheRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheRepository(IConnectionMultiplexer connectionMultiplexer)
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
