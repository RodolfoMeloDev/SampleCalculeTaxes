using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculateTaxes.Domain.Interfaces.Services;

namespace CalculateTaxes.Services.Services
{
    public class CalculateTaxesService(IFeatureFlagService featureFlagService) : ICalculateTaxesService
    {
        private readonly IFeatureFlagService _featureFlagService = featureFlagService;
        public async Task<decimal> ReturnValueTax(decimal value)
        {
            var result = await _featureFlagService.GetByNameFeatureFlag("ReformaTributaria");

            if (result != null && result.Active)
                return value * 0.2m;

            return value * 0.3m;
        }
    }
}