using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class ClientRepository(AppDBContext context, ICacheRepository cache) : RepositoryBase<ClientEntity>(context, cache), IClientRepository
    {
        public async Task<ClientEntity?> GetByCPFAsync(string cpf)
        {
            var cacheData = await _cache.GetCacheAsync<ClientEntity>(GenerateKey(nameof(cpf), cpf));
            if (cacheData != null)
                return cacheData;

            var result = await _dataSet.Where(f => f.CPF.Equals(cpf)).FirstOrDefaultAsync();

            if (result != null)
                await _cache.AddCacheAsync(GenerateKey(nameof(cpf), cpf), result);

            return result;
        }
    }
}