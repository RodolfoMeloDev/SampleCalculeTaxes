using CalculateTaxes.Domain.Dtos.FeatureFlag;

namespace CalculateTaxes.Domain.Interfaces.Services
{
    public interface IFeatureFlagService
    {
        Task<FeatureFlagResponse> CreateFeatureFlag(FeatureFlagCreate createDto);
        Task<FeatureFlagResponse> UpdateFeatureFlag(FeatureFlagUpdate updateDto);
        Task<FeatureFlagResponse?> GetByIdFeatureFlag(int id);
        Task<FeatureFlagResponse?> GetByNameFeatureFlag(string name);
        Task<IEnumerable<FeatureFlagResponse>> GetAllFeatureFlags();
    }
}