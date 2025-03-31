using Bogus;
using CalculateTaxes.Domain.Dtos.FeatureFlag;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Services.Test.Fakers.FeatureFlag
{
    public class FeatureFlagCreateEntityFaker : Faker<FeatureFlagEntity>
    {
        public FeatureFlagCreateEntityFaker()
        {
            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Name, f => f.Name.FullName());
            RuleFor(r => r.Active, true);
            RuleFor(r => r.CreatedAt, DateTime.Now);
        }

        public FeatureFlagCreateEntityFaker(FeatureFlagCreate createDto)
        {
            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Name, createDto.Name);
            RuleFor(r => r.Active, true);
            RuleFor(r => r.CreatedAt, DateTime.Now);
        }
    }
}