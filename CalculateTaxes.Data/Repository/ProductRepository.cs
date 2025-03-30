using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class ProductRepository(AppDBContext context) : RepositoryBase<ProductEntity>(context), IProductRepository
    {
        public async Task<ProductEntity?> GetByNameAsync(string name)
        {
            return await _dataSet.Where(f => f.Name.Equals(name)).FirstOrDefaultAsync();
        }
    }
}