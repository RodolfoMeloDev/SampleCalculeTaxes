using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepositoryBase<ProductEntity>
    {
        Task<ProductEntity?> GetByNameAsync(string name);
    }
}