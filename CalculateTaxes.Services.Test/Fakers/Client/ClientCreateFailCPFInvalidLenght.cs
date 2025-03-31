using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientCreateFailCPFInvalidLenght : Faker<ClientCreate>
    {
        public ClientCreateFailCPFInvalidLenght()
        {
            CustomInstantiator(f => new ClientCreate(
                f.Name.FullName(),
                f.Date.PastDateOnly(Birthday.MaxAge, DateOnly.FromDateTime(DateTime.Today)),
                f.Random.String2(5, "0123456789")
            ));
        }
    }
}