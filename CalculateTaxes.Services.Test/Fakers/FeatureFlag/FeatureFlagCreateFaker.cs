using Bogus;
using CalculateTaxes.Domain.Dtos.FeatureFlag;

namespace CalculateTaxes.Services.Test.Fakers.FeatureFlag
{
    public class FeatureFlagCreateFaker : Faker<FeatureFlagCreate>
    {
        public FeatureFlagCreateFaker()
        {
            CustomInstantiator(f => new FeatureFlagCreate(
                f.Commerce.ProductName(),
                true
            ));
        }
    }
}