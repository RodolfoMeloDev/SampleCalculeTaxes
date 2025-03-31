using Bogus;
using CalculateTaxes.Domain.Dtos.Product;

namespace CalculateTaxes.Services.Test.Fakers.Product
{
    public class ProductCreateFaker : Faker<ProductCreate>
    {
        public ProductCreateFaker()
        {
            CustomInstantiator(f => new ProductCreate(
                f.Commerce.ProductName())
            );
        }  
    }
}