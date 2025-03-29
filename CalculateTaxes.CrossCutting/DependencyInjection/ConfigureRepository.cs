using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculateTaxes.Data.Context;
using CalculateTaxes.Data.Repository;
using CalculateTaxes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CalculateTaxes.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            // Banco de dados espera sempre um serviço de scopo
            serviceCollection.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<IClientRepository, ClientRepository>();
            serviceCollection.AddScoped<IFeatureFlagRepository, FeatureFlagRepository>();

            serviceCollection.AddDbContext<AppDBContext>(
                    options => options.UseInMemoryDatabase("AppDB")
                );
        }
    }
}