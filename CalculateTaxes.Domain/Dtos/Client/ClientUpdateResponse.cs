namespace CalculateTaxes.Domain.Dtos.Client
{
    public record ClientUpdateResponse(int Id, string Name, DateOnly Birthday, bool Active, DateTime CreatedAt, DateTime UpdatedAt);
}