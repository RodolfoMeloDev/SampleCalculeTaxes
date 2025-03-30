using CalculateTaxes.Domain.Dtos.Product;

namespace CalculateTaxes.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProduct(ProductCreate createDto);
        Task<ProductResponse> UpdateProduct(ProductUpdate updateDto);
        Task<ProductResponse?> GetByIdProduct(int id);
        Task<IEnumerable<ProductResponse>> GetAllProducts();
    }
}