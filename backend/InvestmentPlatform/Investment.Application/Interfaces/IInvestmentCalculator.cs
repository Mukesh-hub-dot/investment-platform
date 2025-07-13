
namespace Investment.Application.Interfaces
{
    public interface IInvestmentCalculator
    {
        string Type { get; }
        (decimal returnAmount, decimal fee) Calculate(decimal amount, decimal percentage);
    }
}
