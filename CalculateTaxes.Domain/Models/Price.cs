namespace CalculateTaxes.Domain.Models
{
    public record Price
    {
        public Price(decimal price)
        {
            Validate(price);
        }

        private static void Validate(decimal price){
            if (price <=0 )
                throw new ArgumentException("O preço deve ser maior que ZERO");
        }
    }
}