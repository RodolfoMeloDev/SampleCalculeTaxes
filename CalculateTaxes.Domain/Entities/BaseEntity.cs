using System.ComponentModel.DataAnnotations;

namespace CalculateTaxes.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}