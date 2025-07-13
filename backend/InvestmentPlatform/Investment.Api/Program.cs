using Investment.Api.Middleware;
using Investment.Application.Interfaces;
using Investment.Application.Services;
using Investment.Infrastructure.Services;
using Investment.Infrastructure.Strategies;
using FluentValidation;
using Investment.Application.DTOs;
using Investment.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// ===========================================
//  Register Controllers + Swagger + Memory Cache
// ===========================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

// ===========================================
//  Register Services
// ===========================================
builder.Services.AddScoped<InvestmentCalculationService>();

// ===========================================
//  Register Strategy Pattern Calculators
// ===========================================
builder.Services.AddScoped<IInvestmentCalculator, CashInvestmentCalculator>();
builder.Services.AddScoped<IInvestmentCalculator, SharesInvestmentCalculator>();
builder.Services.AddScoped<IInvestmentCalculator, PropertyInvestmentCalculator>();
builder.Services.AddScoped<IInvestmentCalculator, TermDepositInvestmentCalculator>();
builder.Services.AddScoped<IInvestmentCalculator, ManagedFundsInvestmentCalculator>();
builder.Services.AddScoped<IInvestmentCalculator, ETFInvestmentCalculator>();
builder.Services.AddScoped<IInvestmentCalculator, InvestmentBondsCalculator>();
builder.Services.AddScoped<IInvestmentCalculator, AnnuitiesInvestmentCalculator>();
builder.Services.AddScoped<IInvestmentCalculator, LICInvestmentCalculator>();
builder.Services.AddScoped<IInvestmentCalculator, REITInvestmentCalculator>();

// ===========================================
//  Register FluentValidation Validator
// ===========================================
builder.Services.AddScoped<IValidator<InvestmentCalculationRequest>, InvestmentCalculationRequestValidator>();

// ===========================================
//  Register HTTP Client for Exchange Rate Service
// ===========================================
builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();

// ===========================================
//  Add CORS to allow frontend access
// ===========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ===========================================
//  Configure Middleware
// ===========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

// Enable CORS before Authorization
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
