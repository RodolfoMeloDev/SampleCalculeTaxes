using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Models;
using CalculateTaxes.Domain.Utils;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientCreateFailNameIsEmptyFaker : Faker<ClientCreate>
    {
        public ClientCreateFailNameIsEmptyFaker()
        {
            CustomInstantiator(f => new ClientCreate(
                string.Empty,
                f.Date.PastDateOnly(Birthday.MaxAge, DateOnly.FromDateTime(DateTime.Today)),
                FunctionsUtils.GenerateCPF()
            ));
        }
    }
}