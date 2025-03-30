using System.ComponentModel.DataAnnotations;

namespace CalculateTaxes.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}