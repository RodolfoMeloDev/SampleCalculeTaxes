using System.ComponentModel.DataAnnotations;

namespace CalculateTaxes.Domain.Entities
{
    public class ClientEntity : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }
        public required DateOnly Birthday { get; set; }
        public required string CPF { get; set; }
    }
}