using FluentValidation;
using Investment.Application.DTOs;
using Investment.Application.Services;
using Investment.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Investment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly InvestmentCalculationService _calculationService;
        private readonly IValidator<InvestmentCalculationRequest> _validator;

        public InvestmentController(
            InvestmentCalculationService calculationService,
            IValidator<InvestmentCalculationRequest> validator)
        {
            _calculationService = calculationService;
            _validator = validator;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] InvestmentCalculationRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                var errorResponse = ApiResponse<InvestmentCalculationResult>.FailResponse(
                    data: null,
                    message: "Validation failed",
                    code: 400,
                    type: "ValidationError",
                    errors: errors
                );

                return new ObjectResult(errorResponse)
                {
                    StatusCode = errorResponse.Error?.Code ?? 400
                };
            }

            var response = await _calculationService.CalculateAsync(request);

            return new ObjectResult(response)
            {
                StatusCode = response.Success ? 200 : response.Error?.Code ?? 500
            };
        }
    }
}
