using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ClientCreateResponse> CreateClient(ClientCreate createDto)
        {
            var entity = _mapper.Map<ClientEntity>(createDto);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<ClientCreateResponse>(result);
        }

        public async Task<IEnumerable<ClientResponse>> GetAllClients()
        {
            return _mapper.Map<IEnumerable<ClientResponse>>(await _repository.GetAllAsync());
        }

        public async Task<ClientResponse?> GetByIdClient(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<ClientResponse?>(entity);
        }

        public async Task<ClientUpdateResponse> UpdateClient(ClientUpdate updateDto)
        {
            var entity = _mapper.Map<ClientEntity>(updateDto);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<ClientUpdateResponse>(result);
        }
    }
}