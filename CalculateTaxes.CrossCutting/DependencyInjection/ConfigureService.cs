using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculateTaxes.Domain.Interfaces.Services;
using CalculateTaxes.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CalculateTaxes.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddTransient<IClientService, ClientService>();
        }
    }
}