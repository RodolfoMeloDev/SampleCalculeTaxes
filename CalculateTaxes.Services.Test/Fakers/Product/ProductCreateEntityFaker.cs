using Bogus;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Services.Test.Fakers.Product
{
    public class ProductCreateEntityFaker : Faker<ProductEntity>
    {
        public ProductCreateEntityFaker()
        {
            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Name, f => f.Commerce.Product());
            RuleFor(r => r.Active, true);
            RuleFor(r => r.CreatedAt, DateTime.Now);
        }

        public ProductCreateEntityFaker(ProductCreate createDto)
        {
            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Name, createDto.Name);
            RuleFor(r => r.Active, true);
            RuleFor(r => r.CreatedAt, DateTime.Now);
        }
    }
}