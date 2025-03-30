using CalculateTaxes.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CalculateTaxes.Services.Services
{
    public class CalculateTaxesService(IFeatureFlagService featureFlagService, ILogger<CalculateTaxesService> logger) : ICalculateTaxesService
    {
        private readonly IFeatureFlagService _featureFlagService = featureFlagService;
        private readonly ILogger<CalculateTaxesService> _logger = logger;
        public async Task<decimal> ReturnValueTax(decimal value)
        {
            var result = await _featureFlagService.GetByNameFeatureFlag("ReformaTributaria");

            if (result != null && result.Active)
            { 
                _logger.LogInformation("Utilizando Regra de Cálculo da Reforma Tributária");
                return value * 0.2m;
            }

            _logger.LogInformation("Utilizando Regra de Cálculo Convêncional");
            return value * 0.3m;
        }
    }
}