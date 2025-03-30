using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class ProductRepository(AppDBContext context, ICacheRepository cache) : RepositoryBase<ProductEntity>(context, cache), IProductRepository
    {
        public async Task<ProductEntity?> GetByNameAsync(string name)
        {
            var cacheData = await _cache.GetCacheAsync<ProductEntity>(GenerateKey(nameof(name), name));
            if (cacheData != null)
                return cacheData;

            var result = await _dataSet.Where(f => f.Name.Equals(name)).FirstOrDefaultAsync();

            if (result != null)
                await _cache.AddCacheAsync(GenerateKey(nameof(name), name), result);

            return result;
        }
    }
}