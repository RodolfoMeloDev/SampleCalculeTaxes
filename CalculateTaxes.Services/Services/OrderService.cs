using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CalculateTaxes.Domain.Dtos.Order;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Interfaces.Services;

namespace CalculateTaxes.Services.Services
{
    public class OrderService(IOrdersRepository repository, IMapper mapper) : IOrderService
    {
        private readonly IOrdersRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<OrderCreateResponse> CreateOrder(OrderCreate createDto)
        {
            var entity = _mapper.Map<OrderEntity>(createDto);
            entity.Status = "Criado";
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<OrderCreateResponse>(result);
        }

        public async Task<OrderResponse?> GetByIdOrder(int id)
        {
            var result = _mapper.Map<OrderResponse?>(await _repository.GetByIdOrderWithItems(id));

            result?.Items.ForEach(i => {
                result.Taxes += i.Price;
            });

            if (result != null)
                result.Taxes = result!.Taxes * 0.3m; 

            return result;
        }

        public async Task<IEnumerable<OrderResponse?>> GetByStatusOrder(string status)
        {
            return _mapper.Map<IEnumerable<OrderResponse?>>(await _repository.GetByStatusOrder(status));
        }
    }
}