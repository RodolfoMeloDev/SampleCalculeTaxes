namespace CalculateTaxes.Domain.Dtos
{
    public class BaseDto
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}