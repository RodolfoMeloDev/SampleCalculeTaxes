using Bogus;
using CalculateTaxes.Data.Context;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Models;
using CalculateTaxes.Domain.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CalculateTaxes.Data.Seed
{
    public class DatabaseInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();

                // Garante que o banco foi criado e está atualizado
                context.Database.EnsureCreated();

                var productFaker = new Faker<ProductEntity>()
                    .RuleFor(r => r.Name, f => f.Commerce.ProductName())
                    .RuleFor(r => r.Active, true)
                    .RuleFor(r => r.CreatedAt, DateTime.Now);

                var clientFaker = new Faker<ClientEntity>()
                    .RuleFor(r => r.Name, f => f.Name.FullName())
                    .RuleFor(r => r.Birthday, f => f.Date.PastDateOnly(Birthday.MaxAge, DateOnly.FromDateTime(DateTime.Today)))
                    .RuleFor(r => r.CPF, f => FunctionsUtils.GenerateCPF())
                    .RuleFor(r => r.Active, true)
                    .RuleFor(r => r.CreatedAt, DateTime.Now);

                var orderFaker = new Faker<OrderEntity>()
                    .RuleFor(r => r.OrderId, f => f.IndexFaker + 1)
                    .RuleFor(r => r.ClientId, f => f.Random.Int(1, 10))
                    .RuleFor(r => r.Status, "Criado")
                    .RuleFor(r => r.Items, (f, order) => 
                        new Faker<ItemsOrderEntity>()
                            .RuleFor(r => r.OrderId, _ => order.OrderId) 
                            .RuleFor(r => r.ProductId, f => f.Random.Int(1, 10))
                            .RuleFor(r => r.Amount, f => f.Random.Int(1, 5))
                            .RuleFor(r => r.Price, f => f.Random.Decimal(0.01m, 100m))
                            .Generate(f.Random.Int(1, 5)))
                    .RuleFor(r => r.Taxes, (f, order) => order.Items.Sum(i => i.Price) * 0.3m);

                var featureFlagFaker = new Faker<FeatureFlagEntity>()
                    .RuleFor(r => r.Name, "ReformaTributaria")
                    .RuleFor(r => r.Active, false);

                var products = productFaker.Generate(10);
                var clients = clientFaker.Generate(10);
                var orders = orderFaker.Generate(10);
                var featureFlag = featureFlagFaker.Generate(1);

                if (!context.Products.Any())
                {
                    context.Products.AddRange(products);
                    context.SaveChanges();
                }

                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(clients);
                    context.SaveChanges();
                }

                if (!context.Orders.Any())
                {
                    context.Orders.AddRange(orders);
                    context.SaveChanges();
                }

                if (!context.FeatureFlags.Any())
                {
                    context.FeatureFlags.AddRange(featureFlag);
                    context.SaveChanges();
                }
            }
        }
    }
}