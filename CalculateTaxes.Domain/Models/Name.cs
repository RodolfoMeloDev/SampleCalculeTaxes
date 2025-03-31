namespace CalculateTaxes.Domain.Models
{
    public record Name
    {
        public static int MaxLength {get; private set;} = 100;
        public Name(string name)
        {
            Validate(name);
        }

        private static void Validate(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("O nome não pode ser nulo ou vazio");

            if (name.Length > MaxLength)
                throw new ArgumentException($"O nome não pode ter mais {MaxLength} caracteres");

        }
    }
}