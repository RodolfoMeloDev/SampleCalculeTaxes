using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Models;
using CalculateTaxes.Services.Test.Fakers.Utils;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientCreateFailMaxAge : Faker<ClientCreate>
    {
        public ClientCreateFailMaxAge()
        {
            CustomInstantiator(f => new ClientCreate(
                f.Name.FullName(),
                f.Date.PastDateOnly(Birthday.MaxAge+1, DateOnly.FromDateTime(DateTime.Now.AddYears(-Birthday.MaxAge-1))),
                FunctionsUtils.GenerateCPF()
            ));
        }
    }
}