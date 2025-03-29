using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class OrderRepository(AppDBContext context) : RepositoryBase<OrderEntity>(context), IOrdersRepository
    {
        public async Task<OrderEntity?> GetByIdOrderWithItems(int id)
        {
            return await _dataSet.Include(i => i.Items).Where(f => f.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrderEntity>> GetByStatusOrder(string status)
        {
            return await _dataSet.Include(i => i.Items).Where(f => f.Status.Equals(status)).ToListAsync();
        }
    }
}