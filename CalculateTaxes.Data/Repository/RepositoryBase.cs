using CalculateTaxes.Data.Context;
using CalculateTaxes.Data.Exception;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class RepositoryBase<T>(AppDBContext context) : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly AppDBContext _context = context;
        private DbSet<T> _dataSet = context.Set<T>();

        public async Task<bool> ExistAsync(int id)
        {
            return await _dataSet.AnyAsync(f => f.Id.Equals(id));
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dataSet.Where(f => f.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataSet.ToListAsync();
        }

        public async Task<T> InsertAsync(T item)
        {
            item.CreatedAt = DateTime.Now;
            item.Active = true;

            _dataSet.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            var result = await GetByIdAsync(item.Id);

            if (result == null)
                throw new IntegrityException("A chave de identificação do objeto não foi encontrada, não foi possível atualizar as informações.");

            item.CreatedAt = result.CreatedAt;
            item.UpdatedAt = DateTime.Now;

            _context.Entry(result).CurrentValues.SetValues(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);

            if (result == null)
                return false;

            _dataSet.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }        
    }
}