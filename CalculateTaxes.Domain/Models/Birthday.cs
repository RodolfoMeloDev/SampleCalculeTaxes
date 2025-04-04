namespace CalculateTaxes.Domain.Models
{
    public record Birthday
    {
        public static int MaxAge {get; private set;} = 120;
        public Birthday(DateOnly birthday)
        {
            Validate(birthday);
        }

        private static void Validate(DateOnly birthday)
        {
            if (birthday > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("A data de nascimento não pode ser maior que a data Atual");

            if (ReturnAge(birthday) > MaxAge)
                throw new ArgumentException("Não é permitido que a data de nascimento seja maior que 120 anos");
        }

        private static int ReturnAge(DateOnly birthday)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            int age = today.Year - birthday.Year;

            if (today < new DateOnly(today.Year, birthday.Month, birthday.Day))
            {
                age--;
            }

            return age;
        }
    }
}