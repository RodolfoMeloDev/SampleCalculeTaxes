using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientCreateFailCPFInvalidCaracteres : Faker<ClientCreate>
    {
        public ClientCreateFailCPFInvalidCaracteres()
        {
            CustomInstantiator(f => new ClientCreate(
                f.Name.FullName(),
                f.Date.PastDateOnly(Birthday.MaxAge, DateOnly.FromDateTime(DateTime.Today)),
                f.Random.String2(11, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
            ));
        }
    }
}