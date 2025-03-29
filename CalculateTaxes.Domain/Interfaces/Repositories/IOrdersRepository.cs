using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Domain.Interfaces.Repositories
{
    public interface IOrdersRepository : IRepositoryBase<OrderEntity>
    {
        Task<IEnumerable<OrderEntity>> GetByStatusOrder(string status);
        Task<OrderEntity?> GetByIdOrderWithItems(int id);
    }
}