using FluentAssertions;
using FluentValidation.TestHelper;
using Investment.Application.DTOs;
using Investment.Application.Interfaces;
using Investment.Application.Services;
using Investment.Application.Validators;
using Investment.Infrastructure.Strategies;
using Microsoft.Extensions.Logging;
using Moq;

namespace Investment.Tests.Services
{
    public class InvestmentCalculationServiceTest
    {
        private readonly Mock<IExchangeRateService> _mockExchangeRateService;
        private readonly List<IInvestmentCalculator> _calculators;
        private readonly InvestmentCalculationService _service;
        private readonly InvestmentCalculationRequestValidator _validator = new();

        public InvestmentCalculationServiceTest()
        {
            _mockExchangeRateService = new Mock<IExchangeRateService>();

            // Mock calculator for Cash
            var cashCalculator = new CashInvestmentCalculator();
            _calculators = new List<IInvestmentCalculator> { cashCalculator };

            _mockExchangeRateService.Setup(s => s.GetAUDtoUSDRateAsync())
                .ReturnsAsync(0.65m);

            var mockLogger = new Mock<ILogger<InvestmentCalculationService>>();

            _service = new InvestmentCalculationService(_calculators, _mockExchangeRateService.Object, mockLogger.Object);
        }

        [Fact]
        public async Task CalculateAsync_ShouldReturnExpectedResult_WhenValidInput()
        {
            var request = new InvestmentCalculationRequest
            {
                TotalAmount = 100000,
                Investments = new List<InvestmentItem>
                {
                    new() { Type = "Cash", Percentage = 50 }
                }
            };

            var response = await _service.CalculateAsync(request);

            response.Success.Should().BeTrue();
            response.Data.TotalProjectedReturnAUD.Should().BeGreaterThan(0);
            response.Data.TotalProjectedReturnUSD.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Should_HaveValidationError_When_InvalidInvestmentType()
        {
            var model = new InvestmentCalculationRequest
            {
                TotalAmount = 100000,
                Investments = new List<InvestmentItem>
            {
                new InvestmentItem
                {
                    Type = "Crypto",
                    Percentage = 100
                }
            }
            };

            var result = _validator.TestValidate(model);

            result.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be("Invalid investment type");
        }
    }
}
