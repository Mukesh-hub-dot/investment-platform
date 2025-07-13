using FluentValidation.TestHelper;
using Investment.Application.DTOs;
using Investment.Application.Validators;

namespace Investment.Tests.Validators
{
    public class InvestmentCalculationRequestValidatorTests
    {
        private readonly InvestmentCalculationRequestValidator _validator;

        public InvestmentCalculationRequestValidatorTests()
        {
            _validator = new InvestmentCalculationRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_TotalAmount_Is_Zero()
        {
            var model = new InvestmentCalculationRequest
            {
                TotalAmount = 0,
                Investments = new List<InvestmentItem>
                {
                    new() { Type = "Cash", Percentage = 100 }
                }
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TotalAmount);
        }

        [Fact]
        public void Should_Have_Error_When_Investments_Are_Empty()
        {
            var model = new InvestmentCalculationRequest
            {
                TotalAmount = 100000,
                Investments = new List<InvestmentItem>()
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Investments);
        }


        [Fact]
        public void Should_Pass_For_Valid_Request()
        {
            var model = new InvestmentCalculationRequest
            {
                TotalAmount = 100000,
                Investments = new List<InvestmentItem>
                {
                    new() { Type = "Cash", Percentage = 50 },
                    new() { Type = "Shares", Percentage = 50 }
                }
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
