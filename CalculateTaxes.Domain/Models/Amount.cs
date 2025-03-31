namespace CalculateTaxes.Domain.Models
{
    public record Amount
    {
        public Amount(int amount)
        {
            Validate(amount);
        }

        private static void Validate(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O campo Amount deve ser MAIOR que ZERO");

        }
    }
}