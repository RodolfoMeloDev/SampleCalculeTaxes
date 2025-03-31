using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculateTaxes.Domain.Interfaces.Repositories
{
    public interface ICacheRepository
    {
        Task<T?> GetCacheAsync<T>(string key);
        Task<IEnumerable<T>?> GetCacheListAsync<T>(string key);
        Task<bool> AddCacheAsync<T>(string key, T value, TimeSpan? expiration = null);
        Task<bool> AddCacheAsync<T>(string key, IEnumerable<T> value, TimeSpan? expiration = null);
        Task<bool> RemoveCacheAsync(string key);
        Task RemoveKeysCacheAsync(string baseKey);
    }
}