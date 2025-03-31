using NSubstitute;
using CalculateTaxes.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using CalculateTaxes.Services.Services;
using CalculateTaxes.Domain.Dtos.FeatureFlag;
using FluentAssertions;
using CalculateTaxes.Services.Test.Fakers.FeatureFlag;
using NSubstitute.ReturnsExtensions;

namespace CalculateTaxes.Services.Test.Services
{
    public class CalculateTaxesServiceTest
    {
        private readonly IFeatureFlagService _featureFlagService;
        private readonly ILogger<CalculateTaxesService> _logger;
        private readonly CalculateTaxesService _service;
        private readonly decimal _taxValue = 100m;

        public CalculateTaxesServiceTest()
        {
            _featureFlagService = Substitute.For<IFeatureFlagService>();
            _logger = Substitute.For<ILogger<CalculateTaxesService>>();

            _service = new CalculateTaxesService(_featureFlagService, _logger);
        }

        [Fact]
        public async Task CalculateTaxes_Actual()
        {
            // arrange
            _featureFlagService.GetByNameFeatureFlag(Arg.Any<string>()).ReturnsNull();

            // act
            var result = await _service.ReturnValueTax(_taxValue);

            // assert
            result.Should().Be(30);
        }

        [Fact]
        public async Task CalculateTaxes_ReformaTributaria_Active()
        {
            // arrange
            var fakerFeatureFlag = new FeatureFlagResponseActiveFaker().Generate();

            _featureFlagService.GetByNameFeatureFlag(Arg.Any<string>()).Returns(Task.FromResult<FeatureFlagResponse?>(fakerFeatureFlag));

            // act
            var result = await _service.ReturnValueTax(_taxValue);

            // assert
            result.Should().Be(20);
        }

        [Fact]
        public async Task CalculateTaxes_ReformaTributaria_Inactive()
        {
            // arrange
            var fakerFeatureFlag = new FeatureFlagResponseInactiveFaker().Generate();

            _featureFlagService.GetByNameFeatureFlag(Arg.Any<string>()).Returns(Task.FromResult<FeatureFlagResponse?>(fakerFeatureFlag));

            // act
            var result = await _service.ReturnValueTax(_taxValue);

            // assert
            result.Should().Be(30);
        }
    }
}