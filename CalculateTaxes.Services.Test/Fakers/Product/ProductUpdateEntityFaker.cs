using Bogus;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Services.Test.Fakers.Product
{
    public class ProductUpdateEntityFaker : Faker<ProductEntity>
    {
        public ProductUpdateEntityFaker(ProductUpdate updateDto)
        {
            RuleFor(r => r.Id, f => updateDto.Id);
            RuleFor(r => r.Name, updateDto.Name);
            RuleFor(r => r.Active, updateDto.Active);
            RuleFor(r => r.CreatedAt, f => f.Date.Past());
            RuleFor(r => r.UpdatedAt, DateTime.Now);
        }
    }
}