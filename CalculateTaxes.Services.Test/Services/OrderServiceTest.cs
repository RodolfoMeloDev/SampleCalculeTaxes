using System.Data;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Interfaces.Services;
using CalculateTaxes.Services.Services;
using CalculateTaxes.Services.Test.Fakers.Orders;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CalculateTaxes.Services.Test.Services
{
    public class OrderServiceTest : ServiceBaseTest
    {
        private readonly IOrdersRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICalculateTaxesService _calculateService;
        private readonly OrderService _service;

        public OrderServiceTest()
        {
            _repository = Substitute.For<IOrdersRepository>();
            _clientRepository = Substitute.For<IClientRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _calculateService = Substitute.For<ICalculateTaxesService>();

            _service = new OrderService(_repository, _clientRepository, _productRepository, _calculateService, _mapper);
        }

        [Fact]
        public async Task GetByGetByStatusOrder_Return_Null()
        {
            // arrange
            var status = "Criado";
            _repository.GetByStatusOrder(Arg.Any<string>()).ReturnsNull();

            // act
            var result = await _service.GetByStatusOrder(status);

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByIdStatusOrder_Return_NotNull()
        {
            // arrange
            var status = "Criado";
            List<OrderEntity> data = [new OrderEntityCreateFaker().Generate()];
            _repository.GetByStatusOrder(Arg.Any<string>()).Returns(Task.FromResult<IEnumerable<OrderEntity>>(data));            

            // act
            var result = await _service.GetByStatusOrder(status);

            // assert
            result.Should().HaveCount(data.Count);
        }

        [Fact]
        public async Task GetByIdOrder_Return_Null()
        {
            // arrange
            var id = 1;
            _repository.GetByIdOrderWithItems(Arg.Any<int>()).ReturnsNull();

            // act
            var result = await _service.GetByIdOrder(id);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdOrder_Return_NotNull()
        {
            // arrange
            var id = 1;
            var data = new OrderEntityCreateFaker().Generate();
            _repository.GetByIdOrderWithItems(Arg.Any<int>()).Returns(Task.FromResult<OrderEntity?>(data));            

            // act
            var result = await _service.GetByIdOrder(id);

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
        public async Task Return_Value_RecalculateTax()
        {
            // arrange
            var orderId = 1;
            var OrderEntityFake = new OrderEntityCreateFaker();
            var dataEntity = OrderEntityFake.Generate();
            var dataUpdateResult = new OrderEntityUpdateFaker(dataEntity).Generate();
            var valueTaxes = dataEntity.Items.Select(i => i.Price).Sum() * OrderEntityFake.Percente;

            _repository.GetByIdOrderWithItems(Arg.Any<int>()).Returns(Task.FromResult<OrderEntity?>(dataEntity));
            _repository.UpdateAsync(Arg.Any<OrderEntity>()).Returns(Task.FromResult(dataUpdateResult));
            _calculateService.ReturnValueTax(Arg.Any<decimal>()).Returns(valueTaxes);

            // act
            var result = await _service.RecalculateTax(orderId);

            // assert
            result.Taxes.Should().Be(valueTaxes);
        }

        [Fact]
        public async Task CreateOrder_Success()
        {
            // arrange
            var orderCreateFake = new OrderCreateFaker();
            var orderCreate = orderCreateFake.Generate();
            var dataEntityFake = new OrderEntityCreateFaker(orderCreate);
            var dataEntity = dataEntityFake.Generate();
            var valueTaxes = dataEntity.Items.Select(i => i.Price).Sum() * dataEntityFake.Percente;

            _repository.AnyOrderId(Arg.Any<int>()).Returns(false);
            _repository.InsertAsync(Arg.Any<OrderEntity>()).Returns(Task.FromResult(dataEntity));

            _clientRepository.ExistAsync(Arg.Any<int>()).Returns(true);
            _productRepository.ExistAsync(Arg.Any<int>()).Returns(true);
            _calculateService.ReturnValueTax(Arg.Any<decimal>()).Returns(valueTaxes);

            // act
            var result = await _service.CreateOrder(orderCreate);

            // assert
            result.Id.Should().BeGreaterThan(0);
            result.Status.Should().BeSameAs("Criado");
        }

        [Fact]
        public async Task CreateOrder_Fail_Duplicate()
        {
            // arrange
            var orderCreateFake = new OrderCreateFaker();
            var orderCreate = orderCreateFake.Generate();
            
            _repository.AnyOrderId(Arg.Any<int>()).Returns(true);
            
            // act
            Func<Task> action = async () => await _service.CreateOrder(orderCreate);

            // assert
            await action.Should().ThrowAsync<DuplicateNameException>().WithMessage($"O pedido {orderCreate.OrderId} já está cadastrado no sistema");
        }

        [Fact]
        public async Task CreateOrder_Fail_Client_NotExist()
        {
            // arrange
            var orderCreateFake = new OrderCreateFaker();
            var orderCreate = orderCreateFake.Generate();
            
            _repository.AnyOrderId(Arg.Any<int>()).Returns(false);
            _clientRepository.ExistAsync(Arg.Any<int>()).Returns(false);
            
            // act
            Func<Task> action = async () => await _service.CreateOrder(orderCreate);

            // assert
            await action.Should().ThrowAsync<ArgumentException>().WithMessage($"O cliente informado não está cadastrado no sistema");
        }

        [Fact]
        public async Task CreateOrder_Fail_Product_NotExist()
        {
            // arrange
            var orderCreateFake = new OrderCreateFaker();
            var orderCreate = orderCreateFake.Generate();
            var productsId = string.Join(",", orderCreate.Items.Select(s => s.ProductId));
            
            _repository.AnyOrderId(Arg.Any<int>()).Returns(false);
            _clientRepository.ExistAsync(Arg.Any<int>()).Returns(true);
            _productRepository.ExistAsync(Arg.Any<int>()).Returns(false);
            
            // act
            Func<Task> action = async () => await _service.CreateOrder(orderCreate);

            // assert
            await action.Should().ThrowAsync<ArgumentException>().WithMessage($"Estes produtos não estão cadastrados no sistema. [{productsId}]*");
        }

        [Fact]
        public async Task CreateOrder_Fail_Product_Price_Invalid()
        {
            // arrange
            var orderCreateFake = new OrderCreateFaker();
            var orderCreate = orderCreateFake.Generate();
            orderCreate.Items[0].Price = 0;
            
            _repository.AnyOrderId(Arg.Any<int>()).Returns(false);
            _clientRepository.ExistAsync(Arg.Any<int>()).Returns(true);
            _productRepository.ExistAsync(Arg.Any<int>()).Returns(true);
            
            // act
            Func<Task> action = async () => await _service.CreateOrder(orderCreate);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage("*O preço deve ser maior que ZERO*");
        }

        [Fact]
        public async Task CreateOrder_Fail_Product_Amount_Invalid()
        {
            // arrange
            var orderCreateFake = new OrderCreateFaker();
            var orderCreate = orderCreateFake.Generate();
            orderCreate.Items[0].Amount = 0;
            
            _repository.AnyOrderId(Arg.Any<int>()).Returns(false);
            _clientRepository.ExistAsync(Arg.Any<int>()).Returns(true);
            _productRepository.ExistAsync(Arg.Any<int>()).Returns(true);
            
            // act
            Func<Task> action = async () => await _service.CreateOrder(orderCreate);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage("*O campo Amount deve ser MAIOR que ZERO*");
        }
    }
}