using Investment.Application.Interfaces;

namespace Investment.Infrastructure.Strategies
{
    public class TermDepositInvestmentCalculator : IInvestmentCalculator
    {
        public string Type => "TermDeposit";

        public (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage)
        {
            decimal roiPercent = 10m;
            decimal returnAmount = amount * (percentage / 100m) * roiPercent / 100m;
            decimal fee = returnAmount * 0.01m; // 1% of ROI
            return (returnAmount, fee);
        }
    }

}
