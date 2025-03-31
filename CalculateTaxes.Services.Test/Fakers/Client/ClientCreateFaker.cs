using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Models;
using CalculateTaxes.Domain.Utils;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientCreateFaker : Faker<ClientCreate>
    {
        public ClientCreateFaker()
        {
            CustomInstantiator(f => new ClientCreate(
                f.Name.FullName(),
                f.Date.PastDateOnly(Birthday.MaxAge, DateOnly.FromDateTime(DateTime.Today)),
                FunctionsUtils.GenerateCPF()
            ));
        }
    }
}