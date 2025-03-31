using System.Data;
using CalculateTaxes.Domain.Entities;
using CalculateTaxes.Domain.Interfaces.Repositories;
using CalculateTaxes.Domain.Utils;
using CalculateTaxes.Services.Services;
using CalculateTaxes.Services.Test.Fakers.Client;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CalculateTaxes.Services.Test.Services
{
    public class ClientServiceTest : ServiceBaseTest
    {
        private readonly IClientRepository _repository;
        private readonly ClientService _service;

        public ClientServiceTest()
        {
            _repository = Substitute.For<IClientRepository>();
            _service = new ClientService(_repository, _mapper);
        }

        [Fact]
        public async Task GetGetAllClients_Empty()
        {
            // arrange
            _repository.GetAllAsync().ReturnsNull();

            // act
            var result = await _service.GetAllClients();

            // assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetGetAllClients_NotEmpty()
        {
            // arrange
            List<ClientEntity> data = [new ClientCreateEntityFaker().Generate()];
            _repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<ClientEntity>>(data));

            // act
            var result = await _service.GetAllClients();

            // assert
            result.Should().HaveCount(data.Count);
        }

        [Fact]
        public async Task GetGetByCPF_IsNull()
        {
            // arrange
            var cpf = FunctionsUtils.GenerateCPF();
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();

            // act
            var result = await _service.GetByCPFClient(cpf);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetGetByCPF_Is_NotNull()        
        {
            // arrange
            ClientEntity data = new ClientCreateEntityFaker().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).Returns(Task.FromResult<ClientEntity?>(data));

            // act
            var result = await _service.GetByCPFClient(data.CPF);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetGetById_IsNull()
        {
            // arrange
            var id = 1;
            _repository.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

            // act
            var result = await _service.GetByIdClient(id);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetGetById_Is_NotNull()
        {
            // arrange
            ClientEntity data = new ClientCreateEntityFaker().Generate();
            _repository.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult<ClientEntity?>(data));

            // act
            var result = await _service.GetByIdClient(data.Id);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateClient_Fail_Duplicate_Client()
        {
            // arrange
            var createDto = new ClientCreateFaker().Generate();
            ClientEntity data = new ClientCreateEntityFaker().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).Returns(Task.FromResult<ClientEntity?>(data));

            // act
            Func<Task> action = async () => await _service.CreateClient(createDto);

            // assert
            await action.Should().ThrowAsync<DuplicateNameException>().WithMessage($"O cliente informado já está cadastrada. Id: {data.Id}");
        }

        [Fact]
        public async Task CreateClient_Fail_Name_IsNullOrEmpty()
        {
            // arrange
            var createDto = new ClientCreateFailNameIsEmptyFaker().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateClient(createDto);

            // assert
            await action.Should().ThrowAsync<ArgumentException>().WithMessage("O nome não pode ser nulo ou vazio");
        }

        [Fact]
        public async Task CreateClient_Fail_MaxLenght_Name()
        {
            // arrange
            var createDto = new ClientCreateFailNameLengthFaker().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateClient(createDto);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage($"O nome não pode ter mais {Domain.Models.Name.MaxLength} caracteres");
        }

        [Fact]
        public async Task CreateClient_Fail_Birthday_Invalid()
        {
            // arrange
            var createDto = new ClientCreateFailBirthdayInvalid().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateClient(createDto);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage("A data de nascimento não pode ser maior que a data Atual");
        }

        [Fact]
        public async Task CreateClient_Fail_MaxAge()
        {
            // arrange
            var createDto = new ClientCreateFailMaxAge().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateClient(createDto);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage("Não é permitido que a data de nascimento seja maior que 120 anos");
        }

        [Fact]
        public async Task CreateClient_Fail_CPF_Is_Empty()
        {
            // arrange
            var createDto = new ClientCreateFailCPFIsEmpty().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateClient(createDto);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage("O CPF não pode ser nulo ou vazio");
        }

        [Fact]
        public async Task CreateClient_Fail_CPF_Not_OnlyNumbers()
        {
            // arrange
            var createDto = new ClientCreateFailCPFInvalidCaracteres().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateClient(createDto);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage("O CPF deve ter somente números");
        }

        [Fact]
        public async Task CreateClient_Fail_CPF_Length_Invalid()
        {
            // arrange
            var createDto = new ClientCreateFailCPFInvalidLenght().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateClient(createDto);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage("O CPF deve ter 11 caracteres");
        }

        [Fact]
        public async Task CreateClient_Fail_CPF_Is_Invalid()
        {
            // arrange
            var createDto = new ClientCreateFailCPFIsInvalid().Generate();
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();

            // act
            Func<Task> action = async () => await _service.CreateClient(createDto);

            // assert
            await action.Should().ThrowAsync<Exception>().WithMessage("O CPF informado é inválido");
        }

        [Fact]
        public async Task CreateClient_Success()
        {
            // arrange
            var createDto = new ClientCreateFaker().Generate();
            var entity = new ClientCreateEntityFaker(createDto).Generate();            
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();
            _repository.InsertAsync(Arg.Any<ClientEntity>()).Returns(Task.FromResult(entity));


            // act
            var result = await _service.CreateClient(createDto);

            // assert
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().BeSameAs(createDto.Name);
            result.Birthday.Should().Be(createDto.Birthday);
            result.CPF.Should().BeSameAs(createDto.CPF);
            result.CreatedAt.Should().Be(entity.CreatedAt);
            result.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public async Task UpdateClient_Success()
        {
            // arrange
            var updateDto = new ClientUpdateFaker().Generate();
            var entity = new ClientUpdateEntityFaker(updateDto).Generate();            
            _repository.GetByCPFAsync(Arg.Any<string>()).ReturnsNull();
            _repository.UpdateAsync(Arg.Any<ClientEntity>()).Returns(Task.FromResult(entity));


            // act
            var result = await _service.UpdateClient(updateDto);

            // assert
            result.Id.Should().Be(updateDto.Id);
            result.Name.Should().BeSameAs(updateDto.Name);
            result.Active.Should().Be(updateDto.Active);
            result.UpdatedAt.Should().BeAfter(result.CreatedAt);
        }
    }
}