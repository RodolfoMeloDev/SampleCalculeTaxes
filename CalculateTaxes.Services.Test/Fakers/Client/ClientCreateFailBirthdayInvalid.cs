using Bogus;
using CalculateTaxes.Domain.Dtos.Client;
using CalculateTaxes.Domain.Utils;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientCreateFailBirthdayInvalid : Faker<ClientCreate>
    {
        public ClientCreateFailBirthdayInvalid()
        {
            CustomInstantiator(f => new ClientCreate(
                f.Name.FullName(),
                f.Date.FutureDateOnly(),
                FunctionsUtils.GenerateCPF()
            ));
        }
    }
}