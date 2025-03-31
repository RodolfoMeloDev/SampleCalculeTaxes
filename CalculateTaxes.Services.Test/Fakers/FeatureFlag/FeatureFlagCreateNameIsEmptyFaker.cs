using Bogus;
using CalculateTaxes.Domain.Dtos.FeatureFlag;

namespace CalculateTaxes.Services.Test.Fakers.FeatureFlag
{
    public class FeatureFlagCreateNameIsEmptyFaker : Faker<FeatureFlagCreate>
    {
        public FeatureFlagCreateNameIsEmptyFaker()
        {
            CustomInstantiator(f => new FeatureFlagCreate(
                string.Empty,
                true
            ));
        }
    }
}