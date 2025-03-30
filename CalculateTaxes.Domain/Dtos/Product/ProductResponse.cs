namespace CalculateTaxes.Domain.Dtos.Product
{
    public record ProductResponse(int Id, string Name, bool Active, DateTime CreatedAt, DateTime? UpdatedAt);    
}