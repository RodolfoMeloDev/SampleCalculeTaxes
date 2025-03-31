using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using CalculateTaxes.Domain.Dtos.Order;
using Xunit;

namespace CalculateTaxes.Services.Test.Fakers.Orders
{
    public class ItemOrderCreateFaker : Faker<ItemOrderCreate>
    {
        public ItemOrderCreateFaker()
        {
            RuleFor(r => r.ProductId, f => f.Random.Int(1000, 2000));
            RuleFor(r => r.Amount, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Price, f => f.Random.Decimal(0.01m, 100));
        }
    }
}