using FluentValidation;
using Investment.Application.DTOs;

namespace Investment.Application.Validators
{
    public class InvestmentCalculationRequestValidator : AbstractValidator<InvestmentCalculationRequest>
    {
        public InvestmentCalculationRequestValidator()
        {
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("TotalAmount must be greater than 0");

            RuleFor(x => x.Investments)
                .NotEmpty().WithMessage("At least one investment is required");

            RuleFor(x => x.Investments.Sum(i => i.Percentage))
                .GreaterThan(0).WithMessage("Total percentage must be greater than 0")
                .LessThanOrEqualTo(100).WithMessage("Total percentage must not exceed 100");

            RuleForEach(x => x.Investments).ChildRules(investment =>
            {
                investment.RuleFor(i => i.Type)
               .NotEmpty().WithMessage("Type is required")
               .Must(type => ValidTypes.Contains(type))
               .WithMessage("Invalid investment type");

                investment.RuleFor(i => i.Percentage)
                    .GreaterThan(0).WithMessage("Percentage must be greater than 0");
            });
        }

        private static readonly string[] ValidTypes = new[]
        {
            "Cash", "Shares", "FixedInterest", "TermDeposit", "ManagedFunds",
            "ETF", "InvestmentBonds", "Annuities", "LIC", "REIT"
        };
    }
}
