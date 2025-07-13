using Investment.Application.Interfaces;

namespace Investment.Infrastructure.Strategies
{
    public class SharesInvestmentCalculator : IInvestmentCalculator
    {
        public string Type => "Shares";

        public (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage)
        {
            decimal roiPercent = percentage <= 70 ? 4.3m : 6m;
            decimal returnAmount = amount * (percentage / 100m) * roiPercent / 100m;
            decimal fee = returnAmount * 0.025m; // 2.5% of ROI
            return (returnAmount, fee);
        }
    }
}
