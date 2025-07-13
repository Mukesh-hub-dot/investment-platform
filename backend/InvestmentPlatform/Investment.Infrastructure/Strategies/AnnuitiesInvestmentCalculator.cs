using Investment.Application.Interfaces;

namespace Investment.Infrastructure.Strategies
{
    public class AnnuitiesInvestmentCalculator : IInvestmentCalculator
    {
        public string Type => "Annuities";

        public (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage)
        {
            decimal roiPercent = 4m;
            decimal returnAmount = amount * (percentage / 100m) * roiPercent / 100m;
            decimal fee = returnAmount * 0.014m; // 1.4% of ROI
            return (returnAmount, fee);
        }
    }

}
