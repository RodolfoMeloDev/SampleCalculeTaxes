using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class FeatureFlagRepository(AppDBContext context) : RepositoryBase<FeatureFlagEntity>(context), IFeatureFlagRepository
    {
        public async Task<FeatureFlagEntity?> GetByNameAsync(string name)
        {
            return await _dataSet.Where(f => f.Name.Equals(name)).FirstOrDefaultAsync();
        }
    }
}