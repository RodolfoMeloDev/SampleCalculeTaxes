namespace CalculateTaxes.Services.Test.Fakers.Utils
{
    public static class FunctionsUtils
    {
        public static string GenerateCPF()
        {
            Random random = new Random();
            int[] cpf = new int[11];

            for (int i = 0; i < 9; i++)
                cpf[i] = random.Next(0, 9);

            cpf[9] = CheckDigit(cpf.Take(9).ToArray());
            cpf[10] = CheckDigit(cpf.Take(10).ToArray());

            return string.Join("", cpf);
        }

        private static int CheckDigit(int[] numbers)
        {
            int soma = 0;
            for (int i = 0; i < numbers.Length; i++)
                soma += numbers[i] * ((numbers.Length + 1) - i);

            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}