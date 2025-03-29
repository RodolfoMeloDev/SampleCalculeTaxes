using CalculateTaxes.Domain.Dtos.Client;

namespace CalculateTaxes.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<ClientCreateResponse> CreateClient(ClientCreate createDto);
        Task<ClientUpdateResponse> UpdateClient(ClientUpdate updateDto);
        Task<ClientResponse?> GetByIdClient(int id);
        Task<IEnumerable<ClientResponse>> GetAllClients();
    }
}