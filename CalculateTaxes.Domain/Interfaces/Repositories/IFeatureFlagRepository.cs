using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Domain.Interfaces.Repositories
{
    public interface IFeatureFlagRepository : IRepositoryBase<FeatureFlagEntity>
    {
        Task<FeatureFlagEntity?> GetByNameAsync(string name);
    }
}