using CalculateTaxes.Domain.Dtos.Product;

namespace CalculateTaxes.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductCreateResponse> CreateProduct(ProductCreate createDto);
        Task<ProductUpdateResponse> UpdateProduct(ProductUpdate updateDto);
        Task<ProductDto?> GetByIdProduct(int id);
        Task<IEnumerable<ProductDto>> GetAllProducts();
    }
}