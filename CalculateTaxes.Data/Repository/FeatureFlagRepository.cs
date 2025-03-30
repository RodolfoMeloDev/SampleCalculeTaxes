using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class FeatureFlagRepository(AppDBContext context, ICacheRepository cache) : RepositoryBase<FeatureFlagEntity>(context, cache), IFeatureFlagRepository
    {
        public async Task<FeatureFlagEntity?> GetByNameAsync(string name)
        {
            var cacheData = await _cache.GetCacheAsync<FeatureFlagEntity>(GenerateKey(nameof(name), name));
            if (cacheData != null)
                return cacheData;

            var result = await _dataSet.Where(f => f.Name.Equals(name)).FirstOrDefaultAsync();

            if (result != null)
                await _cache.AddCacheAsync(GenerateKey(nameof(name), name), result);

            return result;
        }
    }
}