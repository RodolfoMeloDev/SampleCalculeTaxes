namespace CalculateTaxes.Domain.Models
{
    public class Name
    {
        public string Value { get; private set; }

        public Name(string name)
        {
            Validate(name);
            this.Value = name.ToLower();
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