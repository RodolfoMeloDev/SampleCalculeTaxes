using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Domain.Dtos.Client
{
    public record ClientResponse(int Id, string Name, DateOnly Birthday, string CPF, bool Active, DateTime CreatedAt, DateTime? UpdatedAt);
}