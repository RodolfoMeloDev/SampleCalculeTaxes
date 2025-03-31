using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CalculateTaxes.CrossCutting.Mappings;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Interfaces.Services;
using CalculateTaxes.Services.Services;
using CalculateTaxes.Services.Test.Fakers.Orders;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace CalculateTaxes.Services.Test.Services
{
    public class OrderServiceTest
    {
        private readonly IOrdersRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICalculateTaxesService _calculateService;
        private readonly IMapper _mapper;
        private readonly OrderService _service;

        public OrderServiceTest()
        {
            _repository = Substitute.For<IOrdersRepository>();
            _clientRepository = Substitute.For<IClientRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _calculateService = Substitute.For<ICalculateTaxesService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToEntityProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            });
            _mapper = config.CreateMapper();

            _service = new OrderService(_repository, _clientRepository, _productRepository, _calculateService, _mapper);
        }

        [Fact]
        public async Task GetByIdOrder_Return_Null()
        {
            // arrange
            _repository.GetByIdOrderWithItems(Arg.Any<int>()).ReturnsNull();

            // act
            var result = await _service.GetByIdOrder(Arg.Any<int>());

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdOrder_Return_NotNull()
        {
            // arrange
            var data = new OrderEntityCreateFaker().Generate();
            _repository.GetByIdOrderWithItems(Arg.Any<int>()).Returns(Task.FromResult<OrderEntity?>(data));            

            // act
            var result = await _service.GetByIdOrder(1);

            // assert
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async Task Return_Exception_RecalculateTax()
        {
            // arrange
            var orderId = 1;
            _repository.GetByIdOrderWithItems(Arg.Any<int>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.RecalculateTax(orderId);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage("Não foi encontrado o item para recalculo");
        }

        [Fact]
        public async Task Return_Value_RecalculateTax_Normal()
        {
            // arrange
            var orderId = 1;
            var dataEntity = new OrderEntityCreateFaker().Generate();
            _repository.GetByIdOrderWithItems(Arg.Any<int>()).Returns(Task.FromResult<OrderEntity?>(dataEntity));

            // act
            var result = await _service.RecalculateTax(orderId);

            // assert
            result.Taxes.Should().Be(dataEntity.Taxes * 0.3m);
        }
    }
}