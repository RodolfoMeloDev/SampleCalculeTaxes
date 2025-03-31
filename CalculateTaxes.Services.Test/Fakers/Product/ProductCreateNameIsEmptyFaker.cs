using Bogus;
using CalculateTaxes.Domain.Dtos.Product;

namespace CalculateTaxes.Services.Test.Fakers.Product
{
    public class ProductCreateNameIsEmptyFaker : Faker<ProductCreate>
    {
        public ProductCreateNameIsEmptyFaker()
        {
            CustomInstantiator(f => new ProductCreate(
                string.Empty)
            );
        }  
    }
}