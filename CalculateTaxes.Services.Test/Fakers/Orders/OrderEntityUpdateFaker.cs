using Bogus;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Services.Test.Fakers.Orders 
{
    public class OrderEntityUpdateFaker : Faker<OrderEntity>
    {
        public decimal Percente { get; private set; } = 0.3m;

        public OrderEntityUpdateFaker(OrderEntity createFaker)
        {
            var items = new ItemOrderEntityFaker().Generate(3);

            RuleFor(r => r.Id, f => createFaker.Id);
            RuleFor(r => r.OrderId, f => createFaker.OrderId);
            RuleFor(r => r.ClientId, f => createFaker.ClientId);
            RuleFor(r => r.Items, createFaker.Items);
            RuleFor(r => r.Taxes, createFaker.Taxes);
            RuleFor(r => r.Active, createFaker.Active);
            RuleFor(r => r.Status, createFaker.Status);
            RuleFor(r => r.CreatedAt, createFaker.CreatedAt);
            RuleFor(r => r.UpdatedAt, DateTime.Now.AddMinutes(1));
        }
    }
}