using Bogus;
using CalculateTaxes.Domain.Dtos.Order;

namespace CalculateTaxes.Services.Test.Fakers.Orders
{
    public class OrderCreateFaker : Faker<OrderCreate>
    {
        public OrderCreateFaker()
        {
            RuleFor(r => r.OrderId, f => f.Random.Int(1, 1000));
            RuleFor(r => r.ClientId, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Items, new ItemOrderCreateFaker().Generate(3) );
        }
    }
}