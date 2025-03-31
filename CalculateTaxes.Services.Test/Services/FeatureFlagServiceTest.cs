using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Models;
using CalculateTaxes.Services.Services;
using CalculateTaxes.Services.Test.Fakers.FeatureFlag;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CalculateTaxes.Services.Test.Services
{
    public class FeatureFlagServiceTest : ServiceBaseTest
    {
        private readonly IFeatureFlagRepository _repository;
        private readonly FeatureFlagService _service;

        public FeatureFlagServiceTest()
        {
            _repository = Substitute.For<IFeatureFlagRepository>();
            _service = new FeatureFlagService(_repository, _mapper);
        }

        [Fact]
        public async Task GetAllFeatureFlags_Empty()
        {
            // arrange
            _repository.GetAllAsync().ReturnsNull();

            // act
            var result = await _service.GetAllFeatureFlags();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllFeatureFlags_NotEmpty()
        {
            // arrange
            List<FeatureFlagEntity> data = [new FeatureFlagCreateEntityFaker().Generate()];
            _repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<FeatureFlagEntity>>(data));

            // act
            var result = await _service.GetAllFeatureFlags();

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
            var result = await _service.GetByIdFeatureFlag(id);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetById_NotEmpty()
        {
            // arrange
            var id = 1;
            FeatureFlagEntity data = new FeatureFlagCreateEntityFaker().Generate();
            _repository.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult<FeatureFlagEntity?>(data));

            // act
            var result = await _service.GetByIdFeatureFlag(id);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetByName_Empty()
        {
            // arrange
            var id = 1;
            _repository.GetByNameAsync(Arg.Any<string>()).ReturnsNull();

            // act
            var result = await _service.GetByIdFeatureFlag(id);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByName_NotEmpty()
        {
            // arrange
            var name = "Item1";
            FeatureFlagEntity data = new FeatureFlagCreateEntityFaker().Generate();
            _repository.GetByNameAsync(Arg.Any<string>()).Returns(Task.FromResult<FeatureFlagEntity?>(data));

            // act
            var result = await _service.GetByNameFeatureFlag(name);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateFeatureFlag_Fail_Duplicate()
        {
            // arrange
            var createDto = new FeatureFlagCreateFaker().Generate();
            var entity = new FeatureFlagCreateEntityFaker().Generate();
            _repository.GetByNameAsync(Arg.Any<string>()).Returns(Task.FromResult<FeatureFlagEntity?>(entity));

            // act
            Func<Task> action = async () => await _service.CreateFeatureFlag(createDto);

            // assert
            await action.Should().ThrowAsync<DuplicateNameException>().WithMessage($"A FeatureFlag informada já está cadastrada. Id: {entity.Id}");
        }

        [Fact]
        public async Task CreateFeatureFlag_Fail_Name_Is_Empty()
        {
            // arrange
            var data = new FeatureFlagCreateNameIsEmptyFaker().Generate();
            _repository.GetByNameAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateFeatureFlag(data);

            // assert
            await action.Should().ThrowAsync<ArgumentException>().WithMessage("O nome não pode ser nulo ou vazio");
        }

        [Fact]
        public async Task CreateProduct_Fail_Name_Max_Lenght_Exceded()
        {
            // arrange
            var data = new FeatureFlagCreateNameMaxLenghtExcededFaker().Generate();
            _repository.GetByNameAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateFeatureFlag(data);

            // assert
            await action.Should().ThrowAsync<ArgumentException>().WithMessage($"O nome não pode ter mais {Name.MaxLength} caracteres");
        }

        [Fact]
        public async Task CreateProduct_Success()
        {
            // arrange
            var createDto = new FeatureFlagCreateFaker().Generate();
            var entity = new FeatureFlagCreateEntityFaker(createDto).Generate();
            _repository.GetByNameAsync(Arg.Any<string>()).ReturnsNull();
            _repository.InsertAsync(Arg.Any<FeatureFlagEntity>()).Returns(Task.FromResult(entity));

            // act
            var result = await _service.CreateFeatureFlag(createDto);

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
            var updateDto = new FeatureFlagUpdateFaker().Generate();
            var entity = new FeatureFlagUpdateEntityFaker(updateDto).Generate();            
            _repository.UpdateAsync(Arg.Any<FeatureFlagEntity>()).Returns(Task.FromResult(entity));

            // act
            var result = await _service.UpdateFeatureFlag(updateDto);

            // assert
            result.Id.Should().Be(updateDto.Id);
            result.Active.Should().Be(updateDto.Active);
            result.UpdatedAt.Should().BeAfter(result.CreatedAt);
        }
    }
}