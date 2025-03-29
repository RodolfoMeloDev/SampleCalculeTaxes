using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Domain.Dtos.Client
{
    public record ClientCreate(string Name, DateOnly Birthday, string CPF);
}