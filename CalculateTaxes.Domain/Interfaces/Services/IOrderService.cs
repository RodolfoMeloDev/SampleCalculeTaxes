using CalculateTaxes.Domain.Dtos.Order;

namespace CalculateTaxes.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderCreateResponse> CreateOrder(OrderCreate createDto);
        Task<OrderResponse?> GetByIdOrder(int id);
        Task<IEnumerable<OrderResponse?>> GetByStatusOrder(string status);
    }
}