namespace CalculateTaxes.Domain.Entities
{
    public class ClientEntity : BaseEntity
    {
        public required string Name { get; set; }
        public required DateOnly Birthday { get; set; }
    }
}