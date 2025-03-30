namespace CalculateTaxes.Domain.Models
{
    public record Amount
    {
        public int Value { get; }
        public Amount(int amount)
        {
            Validate(amount);
            Value = amount;
        }

        private static void Validate(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O campo Amount deve ser MAIOR que ZERO");

        }
    }
}