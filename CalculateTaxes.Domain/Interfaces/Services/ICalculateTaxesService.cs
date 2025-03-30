namespace CalculateTaxes.Domain.Interfaces.Services
{
    public interface ICalculateTaxesService
    {
        Task<decimal> ReturnValueTax(decimal value);
    }
}