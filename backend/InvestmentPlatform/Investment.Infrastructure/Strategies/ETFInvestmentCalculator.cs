using Investment.Application.Interfaces;

namespace Investment.Infrastructure.Strategies
{
    public class ETFInvestmentCalculator : IInvestmentCalculator
    {
        public string Type => "ETF";

        public (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage)
        {
            decimal roiPercent = percentage <= 40 ? 5m : 12.8m;
            decimal returnAmount = amount * (percentage / 100m) * roiPercent / 100m;
            decimal fee = returnAmount * 0.02m; // 2% of ROI
            return (returnAmount, fee);
        }
    }
}
