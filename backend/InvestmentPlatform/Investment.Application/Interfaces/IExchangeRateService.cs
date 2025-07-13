
namespace Investment.Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task<decimal> GetAUDtoUSDRateAsync();
    }
}
