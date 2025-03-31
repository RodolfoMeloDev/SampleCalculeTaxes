using Bogus;
using CalculateTaxes.Domain.Dtos.FeatureFlag;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Services.Test.Fakers.FeatureFlag
{
    public class FeatureFlagCreateNameMaxLenghtExcededFaker : Faker<FeatureFlagCreate>
    {
        public FeatureFlagCreateNameMaxLenghtExcededFaker()
        {
            CustomInstantiator(f => new FeatureFlagCreate(
                new string('a', Name.MaxLength + 1),
                true
            ));
        }
    }
}