using Investment.Application.Interfaces;

namespace Investment.Infrastructure.Strategies
{
    public class ManagedFundsInvestmentCalculator : IInvestmentCalculator
    {
        public string Type => "ManagedFunds";

        public (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage)
        {
            decimal roiPercent = 12m;
            decimal returnAmount = amount * (percentage / 100m) * roiPercent / 100m;
            decimal fee = returnAmount * 0.003m; // 0.3% of ROI
            return (returnAmount, fee);
        }
    }

}
