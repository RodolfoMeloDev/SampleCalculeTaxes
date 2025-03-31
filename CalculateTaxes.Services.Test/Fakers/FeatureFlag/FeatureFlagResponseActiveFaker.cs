using Bogus;
using CalculateTaxes.Domain.Dtos.FeatureFlag;

namespace CalculateTaxes.Services.Test.Fakers.FeatureFlag
{
    public class FeatureFlagResponseActiveFaker : Faker<FeatureFlagResponse>
    {
        public FeatureFlagResponseActiveFaker()
        {
            CustomInstantiator(f => new FeatureFlagResponse(
                f.Random.Int(1, 100),     // Id
                f.Lorem.Word(),           // Name
                true,          // Active
                DateTime.Now,             // CreatedAt
                null      // UpdatedAt (pode ser null)
            ));
        }        
    }
}