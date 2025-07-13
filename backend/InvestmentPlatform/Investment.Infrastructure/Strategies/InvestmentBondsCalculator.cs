using Investment.Application.Interfaces;

namespace Investment.Infrastructure.Strategies
{
    public class InvestmentBondsCalculator : IInvestmentCalculator
    {
        public string Type => "InvestmentBonds";

        public (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage)
        {
            decimal roiPercent = 8m;
            decimal returnAmount = amount * (percentage / 100m) * roiPercent / 100m;
            decimal fee = returnAmount * 0.009m; // 0.9% of ROI
            return (returnAmount, fee);
        }
    }
}
