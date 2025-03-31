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
            List<ItemsOrderEntity> itemsOrders = [];
            createFaker.Items.ForEach(item => {
                itemsOrders.Add( new ItemsOrderEntity
                {
                    Product = new ProductEntity { Name = $"ProductName_{item.ProductId}" },
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    Price = item.Price
                } );
            });

            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.OrderId, createFaker.OrderId);
            RuleFor(r => r.ClientId, createFaker.ClientId);
            RuleFor(r => r.Items, itemsOrders);
            RuleFor(r => r.Active, true);
            RuleFor(r => r.Status, "Criado");
            RuleFor(r => r.CreatedAt, DateTime.Now);
        }
    }
}