namespace CalculateTaxes.Domain.Dtos.Product
{
    public record ProductDto(int Id,
                             string Name,
                             decimal Price,
                             bool Active,
                             DateTime CreatedAt,
                             DateTime? UpdatedAt);    
}