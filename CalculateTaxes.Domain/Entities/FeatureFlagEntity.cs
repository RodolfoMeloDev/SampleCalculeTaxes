using System.ComponentModel.DataAnnotations;

namespace CalculateTaxes.Domain.Entities
{
    public class FeatureFlagEntity : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }
    }
}