using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;

namespace CalculateTaxes.Data.Repository
{
    public class ClientRepository : RepositoryBase<ClientEntity>, IClientRepository
    {
        public ClientRepository(AppDBContext context) : base(context)
        {
        }
    }
}