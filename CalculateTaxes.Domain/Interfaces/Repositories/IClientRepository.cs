using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Domain.Interfaces.Repositories
{
    public interface IClientRepository : IRepositoryBase<ClientEntity>
    {
        Task<ClientEntity?> GetByCPFAsync(string cpf);
    }
}