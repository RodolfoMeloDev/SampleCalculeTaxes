using Bogus;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Services.Test.Fakers.Orders
{
    public class ItemOrderEntityFaker : Faker<ItemsOrderEntity>
    {
        public ItemOrderEntityFaker()
        {
            RuleFor(r => r.ProductId, f => f.Random.Int(1000, 2000));
            RuleFor(r => r.Amount, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Price, f => f.Random.Decimal(0.01m, 100));
        }
    }
}