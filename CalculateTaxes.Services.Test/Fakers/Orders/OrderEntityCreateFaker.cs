using Bogus;
using CalculateTaxes.Domain.Dtos.Order;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Services.Test.Fakers.Orders
{
    public class OrderEntityCreateFaker : Faker<OrderEntity>
    {
        public decimal Percente { get; private set; } = 0.3m;
        public OrderEntityCreateFaker()
        {
            var items = new ItemOrderEntityFaker().Generate(3);

            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.OrderId, f => f.Random.Int(1, 1000));
            RuleFor(r => r.ClientId, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Items, items);
            RuleFor(r => r.Taxes, items.Select(s => s.Price).Sum() * Percente);
            RuleFor(r => r.Active, true);
            RuleFor(r => r.Status, "Criado");
            RuleFor(r => r.CreatedAt, DateTime.Now);
        }

        public OrderEntityCreateFaker(OrderCreate createFaker)
        {
            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.OrderId, createFaker.OrderId);
            RuleFor(r => r.ClientId, createFaker.ClientId);
            RuleFor(r => r.Items, (f, order) => 
                new Faker<ItemsOrderEntity>()
                        .RuleFor(r => r.OrderId, _ => order.OrderId) 
                        .RuleFor(r => r.ProductId, f => f.Random.Int(1, 10))
                        .RuleFor(r => r.Amount, f => f.Random.Int(1, 5))
                        .RuleFor(r => r.Price, f => f.Random.Decimal(0.01m, 100m))
                        .Generate(f.Random.Int(1, 5)))
                .RuleFor(r => r.Taxes, (f, order) => order.Items.Sum(i => i.Price) * 0.3m);
            RuleFor(r => r.Active, true);
            RuleFor(r => r.Status, "Criado");
            RuleFor(r => r.CreatedAt, DateTime.Now);
        }
    }
}