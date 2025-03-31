using CalculateTaxes.Domain.Interfaces.Repositories;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CalculateTaxes.Data.Repository
{
    public class CacheRepositorory(IConnectionMultiplexer redis) : ICacheRepository
    {
        private readonly IDatabase _db = redis.GetDatabase();
        private readonly IServer _server = redis.GetServer(redis.GetEndPoints().First());

        public async Task<bool> AddCacheAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            return await _db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiration);
        }

        public async Task<bool> AddCacheAsync<T>(string key, IEnumerable<T> value, TimeSpan? expiration = null)
        {
            return await _db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiration);
        }

        public async Task<bool> RemoveCacheAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        public async Task<T?> GetCacheAsync<T>(string key)
        {
            var cachedData = await _db.StringGetAsync(key);
            if (cachedData.IsNullOrEmpty) 
                return default;
            
            return JsonConvert.DeserializeObject<T>(cachedData!);
        }

        public async Task<IEnumerable<T>?> GetCacheListAsync<T>(string key)
        {
            var cachedData = await _db.StringGetAsync(key);
            if (cachedData.IsNullOrEmpty) 
                return null;

            return JsonConvert.DeserializeObject<IEnumerable<T>>(cachedData!);
        }

        public async Task RemoveKeysCacheAsync(string baseKey)
        {
            var keys = _server.Keys(pattern: $"{baseKey}:*").ToList();

            foreach (var key in keys)
            {
                await _db.KeyDeleteAsync(key);
            }
        }
    }
}