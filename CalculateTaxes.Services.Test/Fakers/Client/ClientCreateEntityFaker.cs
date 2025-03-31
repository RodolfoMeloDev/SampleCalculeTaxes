using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Models;
using CalculateTaxes.Domain.Utils;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientCreateEntityFaker : Faker<ClientEntity>
    {
        public ClientCreateEntityFaker()
        {
            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Name, f => f.Name.FullName());
            RuleFor(r => r.Birthday, f => f.Date.PastDateOnly(Birthday.MaxAge, DateOnly.FromDateTime(DateTime.Today)));
            RuleFor(r => r.CPF, f => FunctionsUtils.GenerateCPF());
            RuleFor(r => r.Active, true);
            RuleFor(r => r.CreatedAt, DateTime.Now);
        }

        public ClientCreateEntityFaker(ClientCreate createDto)
        {
            RuleFor(r => r.Id, f => f.Random.Int(1, 1000));
            RuleFor(r => r.Name, createDto.Name);
            RuleFor(r => r.Birthday, createDto.Birthday);
            RuleFor(r => r.CPF, createDto.CPF);
            RuleFor(r => r.Active, true);
            RuleFor(r => r.CreatedAt, DateTime.Now);
        }
    }
}