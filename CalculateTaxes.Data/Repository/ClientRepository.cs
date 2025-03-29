using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Repository
{
    public class ClientRepository(AppDBContext context) : RepositoryBase<ClientEntity>(context), IClientRepository
    {
        public async Task<ClientEntity?> GetByCPFAsync(string cpf)
        {
            return await _dataSet.Where(f => f.CPF.Equals(cpf)).FirstOrDefaultAsync();
        }
    }
}