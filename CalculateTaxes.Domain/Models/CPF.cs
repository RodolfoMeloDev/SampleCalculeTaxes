using System.Text.RegularExpressions;

namespace CalculateTaxes.Domain.Models
{
    public class CPF
    {
        public CPF(string cpf)
        {
            Validate(cpf);
        }

        private static void Validate(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                throw new ArgumentException("O CPF não pode ser nulo ou vazio");

            if (!Regex.IsMatch(cpf, @"^\d+$"))
                throw new ArgumentException("O CPF deve ter somente números");

            if (cpf.Length != 11)
                throw new ArgumentException("O CPF deve ter 11 caracteres");

            if (!IsValidCpf(cpf))
                throw new ArgumentException("O CPF informado é inválido");
        }

        private static bool StringCharsEquals(string cnpjCpf)
        {
            bool existDiferentNumbers = false;
            char lastChar = cnpjCpf[0];

            //valida se todos os caracteres são iguais, caso positivo o valor é inválido
            for (int i = 1; i < cnpjCpf.Length; i++)
            {
                if (!lastChar.Equals(cnpjCpf[i]))
                {
                    existDiferentNumbers = true;
                    break;
                }
            }

            if (!existDiferentNumbers)
                return false;

            return true;
        }

        public static bool IsValidCpf(string cpf)
        {
            if (!StringCharsEquals(cpf))
                return false;

            int[] numberValidateDigitOne = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] numberValidateDigitTwo = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int valueCalculeCPF = 0;

            // regra de validação. Cada número do CPF é multiplacada por um determinado valor e se chega no valor total sem considerar os digitos
            // com o valor total divide por 11. Se o resto for 0 ou 1 o primeiro digito é 0 senão o digito será a substrcação de 11 pelo resto da divisão
            for (int i = 0; i < cpf.Length - 2; i++)
            {
                valueCalculeCPF += Convert.ToInt16(Convert.ToString(cpf[i])) * numberValidateDigitOne[i];
            }
            int valueDigitOne = (valueCalculeCPF % 11) < 2 ? 0 : 11 - (valueCalculeCPF % 11);

            if (Convert.ToInt16(Convert.ToString(cpf[9])) != valueDigitOne)
                return false;

            valueCalculeCPF = 0;
            for (int i = 0; i < cpf.Length - 1; i++)
            {
                valueCalculeCPF += Convert.ToInt16(Convert.ToString(cpf[i])) * numberValidateDigitTwo[i];
            }
            int valueDigitTwo = (valueCalculeCPF % 11) < 2 ? 0 : 11 - (valueCalculeCPF % 11);

            if (Convert.ToInt16(Convert.ToString(cpf[10])) != valueDigitTwo)
                return false;

            return true;
        }
    }
}