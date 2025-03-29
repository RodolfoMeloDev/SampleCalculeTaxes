using CalculateTaxes.Domain.Dtos.Client;

namespace CalculateTaxes.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<ClientResponse> CreateClient(ClientCreate createDto);
        Task<ClientResponse> UpdateClient(ClientUpdate updateDto);
        Task<ClientResponse?> GetByIdClient(int id);
        Task<ClientResponse?> GetByCPFClient(string cpf);
        Task<IEnumerable<ClientResponse>> GetAllClients();
    }
}