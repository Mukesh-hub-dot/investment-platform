using Investment.Application.Interfaces;

namespace Investment.Infrastructure.Strategies
{
    public class REITInvestmentCalculator : IInvestmentCalculator
    {
        public string Type => "REIT";

        public (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage)
        {
            decimal roiPercent = 4m;
            decimal returnAmount = amount * (percentage / 100m) * roiPercent / 100m;
            decimal fee = returnAmount * 0.02m; // 2% of ROI
            return (returnAmount, fee);
        }
    }
}
