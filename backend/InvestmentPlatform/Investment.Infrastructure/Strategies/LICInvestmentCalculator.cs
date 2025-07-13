using Investment.Application.Interfaces;

namespace Investment.Infrastructure.Strategies
{
    public class LICInvestmentCalculator : IInvestmentCalculator
    {
        public string Type => "LIC";

        public (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage)
        {
            decimal roiPercent = 6m;
            decimal returnAmount = amount * (percentage / 100m) * roiPercent / 100m;
            decimal fee = returnAmount * 0.013m; // 1.3% of ROI
            return (returnAmount, fee);
        }
    }
}
