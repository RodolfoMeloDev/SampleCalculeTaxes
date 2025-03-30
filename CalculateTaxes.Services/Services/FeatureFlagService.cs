using System.Data;
using AutoMapper;
using CalculateTaxes.Domain.Dtos.FeatureFlag;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Interfaces.Services;
using CalculateTaxes.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace CalculateTaxes.Services.Services
{
    public class FeatureFlagService(IFeatureFlagRepository repository, IMapper mapper) : IFeatureFlagService
    {
        private readonly IFeatureFlagRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<FeatureFlagResponse> CreateFeatureFlag(FeatureFlagCreate createDto)
        {
            var featureFlag = await _repository.GetByNameAsync(createDto.Name);

            if (featureFlag != null)
                throw new DuplicateNameException($"A FeatureFlag informada já está cadastrada. Id: {featureFlag.Id}");

            _ = new Name(createDto.Name);

            var entity = _mapper.Map<FeatureFlagEntity>(createDto);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<FeatureFlagResponse>(result);
        }

        public async Task<IEnumerable<FeatureFlagResponse>> GetAllFeatureFlags()
        {
            return _mapper.Map<IEnumerable<FeatureFlagResponse>>(await _repository.GetAllAsync());
        }

        public async Task<FeatureFlagResponse?> GetByIdFeatureFlag(int id)
        {
            return _mapper.Map<FeatureFlagResponse?>(await _repository.GetByIdAsync(id));
        }

        public async Task<FeatureFlagResponse?> GetByNameFeatureFlag(string name)
        {
            return _mapper.Map<FeatureFlagResponse?>(await _repository.GetByNameAsync(name));
        }

        public async Task<FeatureFlagResponse> UpdateFeatureFlag(FeatureFlagUpdate updateDto)
        {
            var entity = _mapper.Map<FeatureFlagEntity>(updateDto);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<FeatureFlagResponse>(result);
        }
    }
}