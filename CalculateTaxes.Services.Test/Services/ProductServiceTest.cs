using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CalculateTaxes.Domain.Dtos.Product;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Models;
using CalculateTaxes.Services.Services;
using CalculateTaxes.Services.Test.Fakers.Product;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace CalculateTaxes.Services.Test.Services
{
    public class ProductServiceTest : ServiceBaseTest
    {
        private readonly IProductRepository _repository;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _repository = Substitute.For<IProductRepository>();
            _service = new ProductService(_repository, _mapper);
        }

        [Fact]
        public async Task GetAllProducts_Empty()
        {
            // arrange
            _repository.GetAllAsync().ReturnsNull();

            // act
            var result = await _service.GetAllProducts();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllClients_NotEmpty()
        {
            // arrange
            List<ProductEntity> data = [new ProductCreateEntityFaker().Generate()];
            _repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<ProductEntity>>(data));

            // act
            var result = await _service.GetAllProducts();

            // assert
            result.Should().HaveCount(data.Count);
        }

        [Fact]
        public async Task GetById_Empty()
        {
            // arrange
            var id = 1;
            _repository.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

            // act
            var result = await _service.GetByIdProduct(id);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetById_NotEmpty()
        {
            // arrange
            var id = 1;
            ProductEntity data = new ProductCreateEntityFaker().Generate();
            _repository.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult<ProductEntity?>(data));

            // act
            var result = await _service.GetByIdProduct(id);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateProduct_Fail_Duplicate()
        {
            // arrange
            var createDto = new ProductCreateFaker().Generate();
            var entity = new ProductCreateEntityFaker().Generate();
            _repository.GetByNameAsync(Arg.Any<string>()).Returns(Task.FromResult<ProductEntity?>(entity));

            // act
            Func<Task> action = async () => await _service.CreateProduct(createDto);

            // assert
            await action.Should().ThrowAsync<DuplicateNameException>().WithMessage($"O Produto informada já está cadastrada. Id: {entity.Id}");
        }

        [Fact]
        public async Task CreateProduct_Fail_Name_Is_Empty()
        {
            // arrange
            var data = new ProductCreateNameIsEmptyFaker().Generate();
            _repository.GetByNameAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateProduct(data);

            // assert
            await action.Should().ThrowAsync<ArgumentException>().WithMessage("O nome não pode ser nulo ou vazio");
        }

        [Fact]
        public async Task CreateProduct_Fail_Name_Max_Lenght_Exceded()
        {
            // arrange
            var data = new ProductCreateNameMaxLenghtInvalidFaker().Generate();
            _repository.GetByNameAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateProduct(data);

            // assert
            await action.Should().ThrowAsync<ArgumentException>().WithMessage($"O nome não pode ter mais {Name.MaxLength} caracteres");
        }

        [Fact]
        public async Task CreateProduct_Success()
        {
            // arrange
            var createDto = new ProductCreateFaker().Generate();
            var entity = new ProductCreateEntityFaker(createDto).Generate();
            _repository.GetByNameAsync(Arg.Any<string>()).ReturnsNull();
            _repository.InsertAsync(Arg.Any<ProductEntity>()).Returns(Task.FromResult(entity));

            // act
            var result = await _service.CreateProduct(createDto);

            // assert
            result.Id.Should().Be(entity.Id);
            result.Name.Should().Be(createDto.Name);
            result.Active.Should().Be(entity.Active);
            result.CreatedAt.Should().Be(entity.CreatedAt);
            result.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public async Task UpdateProduct_Success()
        {
            // arrange
            var updateDto = new ProductUpdateFaker().Generate();
            var entity = new ProductUpdateEntityFaker(updateDto).Generate();            
            _repository.UpdateAsync(Arg.Any<ProductEntity>()).Returns(Task.FromResult(entity));

            // act
            var result = await _service.UpdateProduct(updateDto);

            // assert
            result.Id.Should().Be(updateDto.Id);
            result.Name.Should().BeSameAs(updateDto.Name);
            result.Active.Should().Be(updateDto.Active);
            result.UpdatedAt.Should().BeAfter(result.CreatedAt);
        }
    }
}