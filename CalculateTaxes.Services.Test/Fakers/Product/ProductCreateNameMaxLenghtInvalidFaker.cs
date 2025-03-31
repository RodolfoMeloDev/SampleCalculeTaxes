using Bogus;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Services.Test.Fakers.Product
{
    public class ProductCreateNameMaxLenghtInvalidFaker : Faker<ProductCreate>
    {
        public ProductCreateNameMaxLenghtInvalidFaker()
        {
            CustomInstantiator(f => new ProductCreate(
                new string('a', Name.MaxLength + 1))
            );
        }  
    }
}