using Bogus;
using CalculateTaxes.Domain.Dtos.FeatureFlag;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Services.Test.Fakers.FeatureFlag
{
    public class FeatureFlagUpdateEntityFaker : Faker<FeatureFlagEntity>
    {
        public FeatureFlagUpdateEntityFaker(FeatureFlagUpdate updateDto)
        {
            RuleFor(r => r.Id, updateDto.Id);
            RuleFor(r => r.Active, updateDto.Active);
            RuleFor(r => r.CreatedAt, f => f.Date.Past());
            RuleFor(r => r.UpdatedAt, DateTime.Now);
        }
    }
}