using System.Data;
using AutoMapper;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Interfaces.Services;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Services.Services
{
    public class ClientService(IClientRepository repository, IMapper mapper) : IClientService
    {
        private readonly IClientRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<ClientResponse> CreateClient(ClientCreate createDto)
        {
            var client = await _repository.GetByCPFAsync(createDto.CPF);

            if (client != null)
                throw new DuplicateNameException($"O cliente informado já está cadastrada. Id: {client.Id}");

            _ = new Name(createDto.Name);
            _ = new Birthday(createDto.Birthday);
            _ = new CPF(createDto.CPF);

            var entity = _mapper.Map<ClientEntity>(createDto);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<ClientResponse>(result);
        }

        public async Task<IEnumerable<ClientResponse>> GetAllClients()
        {
            return _mapper.Map<IEnumerable<ClientResponse>>(await _repository.GetAllAsync());
        }

        public async Task<ClientResponse?> GetByCPFClient(string cpf)
        {
            var entity = await _repository.GetByCPFAsync(cpf);
            return _mapper.Map<ClientResponse?>(entity);
        }

        public async Task<ClientResponse?> GetByIdClient(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ClientResponse?>(entity);
        }

        public async Task<ClientResponse> UpdateClient(ClientUpdate updateDto)
        {
            var entity = _mapper.Map<ClientEntity>(updateDto);
            var result = await _repository.UpdateAsync(entity);

            _ = new Name(updateDto.Name);

            return _mapper.Map<ClientResponse>(result);
        }
    }
}