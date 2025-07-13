using Investment.Application.Interfaces;

public class CashInvestmentCalculator : IInvestmentCalculator
{
    public string Type => "Cash";

    public (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage)
    {
        decimal roiPercent = percentage <= 50 ? 8.5m : 10m;
        decimal returnAmount = amount * (percentage / 100m) * roiPercent / 100m;
        decimal fee = percentage <= 50 ? returnAmount * 0.05m : 0; // 5% of ROI or Waived
        return (returnAmount, fee);
    }
}
