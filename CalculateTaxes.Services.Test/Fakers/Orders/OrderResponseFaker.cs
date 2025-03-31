using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using CalculateTaxes.Domain.Dtos.Order;

namespace CalculateTaxes.Services.Test.Fakers.Orders
{
    public class OrderResponseFaker : Faker<OrderResponse>
    {
        public OrderResponseFaker()
        {
            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.OrderId, f => f.Random.Int(1, 1000));
            RuleFor(r => r.ClientId, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Taxes, f => f.Random.Decimal(1, 1000));
            RuleFor(r => r.Status, "Criado");
            RuleFor(r => r.Items, new ItemOrderCreateFaker().Generate(3) );
        }
    }
}