using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientCreateFailCPFIsEmpty : Faker<ClientCreate>
    {
        public ClientCreateFailCPFIsEmpty()
        {
            CustomInstantiator(f => new ClientCreate(
                f.Name.FullName(),
                f.Date.PastDateOnly(Birthday.MaxAge, DateOnly.FromDateTime(DateTime.Today)),
                string.Empty
            ));
        }
    }
}