using CalculateTaxes.Data.Context;
using CalculateTaxes.Data.Exception;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class RepositoryBase<T>(AppDBContext context, ICacheRepository cache) : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly AppDBContext _context = context;
        protected readonly ICacheRepository _cache = cache;
        protected DbSet<T> _dataSet = context.Set<T>();

        private string _keyItem = $"{typeof(T)}:Item";
        protected string _keyAll = $"{typeof(T)}:List";

        public async Task<bool> ExistAsync(int id)
        {
            var cacheData = await _cache.GetCacheAsync<T>(GenerateKey("ExistAsync", id));
            if (cacheData != null)
                return true;

            var result = await _dataSet.AnyAsync(f => f.Id.Equals(id));

            await _cache.AddCacheAsync(GenerateKey("ExistAsync", id), result);

            return result;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var cacheData = await _cache.GetCacheAsync<T>(GenerateKey("Id", id));
            if (cacheData != null)
                return cacheData;

            var result = await _dataSet.Where(f => f.Id.Equals(id)).FirstOrDefaultAsync();

            if (result != null)
                await _cache.AddCacheAsync(GenerateKey("Id", id), result);

            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var cacheData = await _cache.GetCacheAsync<IEnumerable<T>>($"{_keyAll}:{nameof(GetAllAsync)}");
            if (cacheData != null)
                return cacheData;

            var result = await _dataSet.ToListAsync();

            if (result != null && result.Count != 0)
                await _cache.AddCacheAsync($"{_keyAll}:{nameof(GetAllAsync)}", result);

            return result ?? [];
        }

        public async Task<T> InsertAsync(T item)
        {
            item.CreatedAt = DateTime.Now;

            _dataSet.Add(item);
            await _context.SaveChangesAsync();

            await _cache.AddCacheAsync(GenerateKey("Id", item.Id), item);
            await _cache.RemoveKeysCacheAsync(_keyAll);

            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            await _cache.RemoveCacheAsync(GenerateKey("Id", item.Id));
            await _cache.RemoveKeysCacheAsync(_keyItem);
            await _cache.RemoveKeysCacheAsync(_keyAll);

            var result = await GetByIdAsync(item.Id);

            if (result == null)
                throw new IntegrityException("A chave de identificação do objeto não foi encontrada, não foi possível atualizar as informações.");

            item.CreatedAt = result.CreatedAt;
            item.UpdatedAt = DateTime.Now;

            _context.Entry(result).CurrentValues.SetValues(item);

            await _context.SaveChangesAsync();
            
            await _cache.RemoveCacheAsync(GenerateKey("Id", item.Id));
            await _cache.AddCacheAsync(GenerateKey("Id", item.Id), item);

            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);

            if (result == null)
                return false;

            _dataSet.Remove(result);
            await _context.SaveChangesAsync();

            await _cache.RemoveCacheAsync(GenerateKey("Id", id));
            await _cache.RemoveKeysCacheAsync(_keyItem);
            await _cache.RemoveKeysCacheAsync(_keyAll);

            return true;
        }        
    
        protected string GenerateKey(string nameKey, object value)
        {
            return $"{_keyItem}:{nameKey}:{value}";
        }
    }
}