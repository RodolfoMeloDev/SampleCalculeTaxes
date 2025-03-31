using Bogus;
using CalculateTaxes.Domain.Dtos.FeatureFlag;

namespace CalculateTaxes.Services.Test.Fakers.FeatureFlag
{
    public class FeatureFlagUpdateFaker : Faker<FeatureFlagUpdate>
    {
        public FeatureFlagUpdateFaker()
        {
            CustomInstantiator(f => new FeatureFlagUpdate(
                f.Random.Int(1, 1000),
                f.Random.Bool()
            ));
        }
    }
}