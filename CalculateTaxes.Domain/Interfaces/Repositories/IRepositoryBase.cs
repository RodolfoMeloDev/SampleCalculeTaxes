using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T: BaseEntity
    {
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> ExistAsync(int id);
    }
}