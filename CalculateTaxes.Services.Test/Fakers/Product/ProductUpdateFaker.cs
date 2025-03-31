using Bogus;
using CalculateTaxes.Domain.Dtos.Product;

namespace CalculateTaxes.Services.Test.Fakers.Product
{
    public class ProductUpdateFaker : Faker<ProductUpdate>
    {
        public ProductUpdateFaker()
        {
            CustomInstantiator(f => new ProductUpdate(
                f.Random.Int(1, 1000),
                f.Commerce.Product(),
                f.Random.Bool()
                )
            );
        }  
    }
}