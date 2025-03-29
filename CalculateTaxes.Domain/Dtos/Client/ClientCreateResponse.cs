namespace CalculateTaxes.Domain.Dtos.Client
{
    public record ClientCreateResponse(int Id, string Name, DateOnly Birthday, DateTime CreatedAt);
}