namespace CalculateTaxes.Domain.Models
{
    public record Name
    {
        public string Value { get; }
        public Name(string name)
        {
            Validate(name);
            Value = name.ToLower();
        }

        private static void Validate(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("O nome não pode ser nulo ou vazio");

            if (name.Length > 100)
                throw new ArgumentException("O nome não pode ter mais 100 caracteres");

        }
    }
}