using Investment.Application.DTOs;
using Investment.Application.Interfaces;
using Investment.Application.Wrappers;
using Microsoft.Extensions.Logging;

namespace Investment.Application.Services
{
    public class InvestmentCalculationService
    {
        private readonly IEnumerable<IInvestmentCalculator> _calculators;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ILogger<InvestmentCalculationService> _logger;

        public InvestmentCalculationService(
            IEnumerable<IInvestmentCalculator> calculators,
            IExchangeRateService exchangeRateService,
            ILogger<InvestmentCalculationService> logger)
        {
            _calculators = calculators;
            _exchangeRateService = exchangeRateService;
            _logger = logger;
        }

        public async Task<ApiResponse<InvestmentCalculationResult>> CalculateAsync(InvestmentCalculationRequest request)
        {
            try
            {
                decimal totalReturn = 0;
                decimal totalFee = 0;

                foreach (var investment in request.Investments)
                {
                    var calculator = _calculators.FirstOrDefault(c =>
                    c.Type.Equals(investment.Type, StringComparison.OrdinalIgnoreCase));

                    if (calculator == null)
                    {
                        return ApiResponse<InvestmentCalculationResult>.FailResponse(
                            null,
                            $"No calculator found for investment type: {investment.Type}",
                            400,
                            "ValidationError"
                        );
                    }

                    // Correct amount calculation for the investment based on percentage
                    var amountForType = request.TotalAmount * (investment.Percentage / 100m);

                    var (returnAmount, fee) = calculator.Calculate(amountForType, investment.Percentage);

                    totalReturn += returnAmount;
                    totalFee += fee;
                }

                var usdRate =  await _exchangeRateService.GetAUDtoUSDRateAsync();

                var result = new InvestmentCalculationResult
                {
                    TotalProjectedReturnAUD = totalReturn,
                    TotalFeeAUD = totalFee,
                    TotalProjectedReturnUSD = Math.Round(totalReturn * usdRate, 2)
                };

                return ApiResponse<InvestmentCalculationResult>.SuccessResponse(result, "Calculation successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Calculation failed.");
                return ApiResponse<InvestmentCalculationResult>.FailResponse(null, "Internal server error", 500, "ServerError");
            }
        }
    }
}
