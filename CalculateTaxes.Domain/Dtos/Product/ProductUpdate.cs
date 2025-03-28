namespace CalculateTaxes.Domain.Dtos.Product
{
    public record ProductUpdate(int id, string Name, decimal Price, bool Active);
}