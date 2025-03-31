using Bogus;
using CalculateTaxes.Domain.Dtos.Client;

namespace CalculateTaxes.Services.Test.Fakers.Client
{
    public class ClientUpdateFaker : Faker<ClientUpdate>
    {
       public ClientUpdateFaker()
        {
            CustomInstantiator(f => new ClientUpdate(
                f.Random.Int(1, 1000),
                f.Name.FullName(),
                f.Random.Bool()
            ));
        }
    }
}