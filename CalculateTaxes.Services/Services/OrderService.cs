using System.Data;
using System.Text;
using AutoMapper;
using CalculateTaxes.Domain.Dtos.Order;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Interfaces.Services;
using CalculateTaxes.Domain.Models;

namespace CalculateTaxes.Services.Services
{
    public class OrderService(IOrdersRepository repository, IClientRepository clientRepository, IProductRepository productRepository, ICalculateTaxesService calculateTaxesService, IMapper mapper) : IOrderService
    {
        private readonly IOrdersRepository _repository = repository;
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly ICalculateTaxesService _calculateTaxesService = calculateTaxesService;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<OrderCreateResponse> CreateOrder(OrderCreate createDto)
        {
            await ValidateOrder(createDto);

            var entity = _mapper.Map<OrderEntity>(createDto);
            entity.Status = "Criado";
            entity.Taxes = await CalculateValueTax(createDto.Items);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<OrderCreateResponse>(result);
        }

        public async Task<OrderResponse?> GetByIdOrder(int id)
        {
            return _mapper.Map<OrderResponse?>(await _repository.GetByIdOrderWithItems(id));
        }

        public async Task<IEnumerable<OrderResponse?>> GetByStatusOrder(string status)
        {
            return _mapper.Map<IEnumerable<OrderResponse?>>(await _repository.GetByStatusOrder(status));
        }

        public async Task<OrderResponse> RecalculateTax(int id)
        {
            var order = await _repository.GetByIdOrderWithItems(id) ?? 
                throw new Exception("Não foi encontrado o item para recalculo");
                
            order.Taxes = await CalculateValueTax(order.Items);

            var result = await _repository.UpdateAsync(order);

            return _mapper.Map<OrderResponse>(result);
        }

        private async Task<decimal> CalculateValueTax(IEnumerable<ItemOrderCreate> items)
        {
            var totalOrder = 0m;
            items.ToList().ForEach(i => {
                totalOrder += i.Price;
            });

            return await _calculateTaxesService.ReturnValueTax(totalOrder);
        }

        private async Task<decimal> CalculateValueTax(IEnumerable<ItemsOrderEntity> items)
        {
            var totalOrder = 0m;
            items.ToList().ForEach(i => {
                totalOrder += i.Price;
            });

            return await _calculateTaxesService.ReturnValueTax(totalOrder);
        }
    
        private void ValidateProduct(IEnumerable<ItemOrderCreate> items, out string productError)
        {   
            productError = string.Empty;

            var productInvalid = new StringBuilder();
            items.ToList().ForEach(async item => {
                if (!await _productRepository.ExistAsync(item.ProductId)){
                    productInvalid.Append($"{item.ProductId},");
                    return;   
                }

                try
                {
                    _ = new Price(item.Price);
                }
                catch (Exception e)
                {
                    productInvalid.AppendLine($"ProdutoId: {item.ProductId}. {e.Message}");
                }

                try
                {
                    _ = new Amount(item.Amount);
                }
                catch (Exception e)
                {
                    productInvalid.AppendLine($"ProdutoId: {item.ProductId}. {e.Message}");
                }
            });

            if (productInvalid.Length > 0 && productInvalid.ToString()[productInvalid.Length-1].Equals(','))
                productInvalid.Length--;
            productError = productInvalid.ToString();
        }

        private async Task ValidateOrder(OrderCreate createDto)
        {
            if (await _repository.AnyOrderId(createDto.OrderId))
                throw new DuplicateNameException($"O pedido {createDto.OrderId} já está cadastrado no sistema");

            if (!await _clientRepository.ExistAsync(createDto.ClientId))
                throw new ArgumentException("O cliente informado não está cadastrado no sistema");

            ValidateProduct(createDto.Items, out var productError);

            if (!string.IsNullOrEmpty(productError))
                throw new ArgumentException($"Estes produtos não estão cadastrados no sistema. [{productError}]");
        }
    }
}