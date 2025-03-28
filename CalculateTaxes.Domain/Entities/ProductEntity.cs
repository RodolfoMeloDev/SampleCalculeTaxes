namespace CalculateTaxes.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }
}