using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Models;
using CalculateTaxes.Domain.Utils;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientCreateFailNameLengthFaker : Faker<ClientCreate>
    {
        public ClientCreateFailNameLengthFaker()
        {
            CustomInstantiator(f => new ClientCreate(
                new string('A', Name.MaxLength + 1),
                f.Date.PastDateOnly(Birthday.MaxAge, DateOnly.FromDateTime(DateTime.Today)),
                FunctionsUtils.GenerateCPF()
            ));
        }
    }
}