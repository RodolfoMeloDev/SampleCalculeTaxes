using Bogus;
using CalculateTaxes.Domain.Dtos.FeatureFlag;

namespace CalculateTaxes.Services.Test.Fakers.FeatureFlag
{
    public class FeatureFlagResponseInactiveFaker : Faker<FeatureFlagResponse>
    {
        public FeatureFlagResponseInactiveFaker()
        {
            CustomInstantiator(f => new FeatureFlagResponse(
                f.Random.Int(1, 100),     // Id
                f.Lorem.Word(),           // Name
                false,          // Active
                DateTime.Now,             // CreatedAt
                null      // UpdatedAt (pode ser null)
            ));
        }        
    }
}