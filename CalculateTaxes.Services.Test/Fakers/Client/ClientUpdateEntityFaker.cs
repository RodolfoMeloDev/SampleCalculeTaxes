using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Entities;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientUpdateEntityFaker : Faker<ClientEntity>
    {
        public ClientUpdateEntityFaker(ClientUpdate updateDto)
        {
            RuleFor(r => r.Id, updateDto.Id);
            RuleFor(r => r.Name, updateDto.Name);
            RuleFor(r => r.Active, updateDto.Active);
            RuleFor(r => r.CreatedAt, f => f.Date.Past());
            RuleFor(r => r.UpdatedAt, DateTime.Now);
        }
    }
}