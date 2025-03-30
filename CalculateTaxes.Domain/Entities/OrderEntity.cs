namespace CalculateTaxes.Domain.Entities
{
    public class OrderEntity : BaseEntity
    {
        public required int OrderId { get; set; }
        public required int ClientId { get; set; }
        public required ClientEntity Client { get; set; }
        public required decimal Taxes { get; set; }
        public required string Status { get; set; }
        public required IEnumerable<ItemsOrderEntity> Items { get; set; }        
    }
}