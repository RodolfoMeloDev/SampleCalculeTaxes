namespace CalculateTaxes.Domain.Dtos.Product
{
    public record ProductCreateResponse(int Id, string Name, decimal Price, DateTime CreatedAt);
}