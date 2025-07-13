using System.Text.Json;
using Investment.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Investment.Infrastructure.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ExchangeRateService> _logger;

        private const string CacheKey = "AUDtoUSD";

        public ExchangeRateService(
            HttpClient httpClient,
            IConfiguration configuration,
            IMemoryCache cache,
            ILogger<ExchangeRateService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cache = cache;
            _logger = logger;
        }

        public async Task<decimal> GetAUDtoUSDRateAsync()
        {
            //  Return from cache if available
            if (_cache.TryGetValue(CacheKey, out decimal cachedRate))
            {
                _logger.LogInformation("Using cached exchange rate: {Rate}", cachedRate);
                return cachedRate;
            }

            try
            {
                var baseUrl = _configuration["ExchangeRateApi:BaseUrl"];
                var apiKey = _configuration["ExchangeRateApi:ApiKey"];
                var url = $"{baseUrl}/convert?to=USD&from=AUD&amount=1";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("apikey", apiKey);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<CurrencyResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result == null || result.Result <= 0)
                    throw new Exception("Invalid response from currency API");

                //  Cache the result for 24 hour
                _cache.Set(CacheKey, result.Result, TimeSpan.FromHours(24));

                return result.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch exchange rate from API");

                //  Fallback to cached value if available
                if (_cache.TryGetValue(CacheKey, out decimal fallbackRate))
                {
                    _logger.LogWarning("Falling back to cached exchange rate: {Rate}", fallbackRate);
                    return fallbackRate;
                }

                //  No fallback, rethrow to middleware
                throw new ApplicationException("Exchange rate API failed and no fallback available.", ex);
            }
        }

        private class CurrencyResponse
        {
            public decimal Result { get; set; }
        }
    }
}
