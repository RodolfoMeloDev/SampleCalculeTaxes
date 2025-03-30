using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class OrderRepository(AppDBContext context, ICacheRepository cache) : RepositoryBase<OrderEntity>(context, cache), IOrdersRepository
    {
        public async Task<bool> AnyOrderId(int orderId)
        {
            var cacheData = await _cache.GetCacheAsync<ProductEntity>(GenerateKey(nameof(orderId), orderId));
            if (cacheData != null)
                return true;

            var result = await _dataSet.AnyAsync(f => f.OrderId.Equals(orderId));

            await _cache.AddCacheAsync(GenerateKey(nameof(orderId), orderId), result);

            return result;
        }

        public async Task<OrderEntity?> GetByIdOrderWithItems(int id)
        {
            var cacheData = await _cache.GetCacheAsync<OrderEntity>(GenerateKey("IdWithItems", id));
            if (cacheData != null)
                return cacheData;

            var result = await _dataSet.Include(i => i.Items).Where(f => f.Id.Equals(id)).FirstOrDefaultAsync();

            if (result != null)
                await _cache.AddCacheAsync(GenerateKey("IdWithItems", id), result);

            return result;
        }

        public async Task<IEnumerable<OrderEntity>> GetByStatusOrder(string status)
        {
            var cacheData = await _cache.GetCacheListAsync<OrderEntity>($"{_keyAll}:{nameof(GetByStatusOrder)}");
            if (cacheData != null)
                return cacheData;

            var result = await _dataSet.Include(i => i.Items).Where(f => f.Status.Equals(status)).ToListAsync();

            if (result != null)
                await _cache.AddCacheAsync($"{_keyAll}:{nameof(GetByStatusOrder)}", result);

            return result ?? [];
        }
    }
}