namespace CalculateTaxes.Domain.Entities
{
    public class ItemsOrderEntity : BaseEntity
    {
        public required int ProductId { get; set; }
        public required ProductEntity Product { get; set; }
        public required int Amount { get; set; }
        public required decimal Price { get; set; }
    }
}