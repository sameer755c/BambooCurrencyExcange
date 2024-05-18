using BambooCurrencyExchangeAPI.Helpers;
using BambooCurrencyExchangeAPI.Models;
using Polly;
using Polly.Retry;

namespace BambooCurrencyExchangeAPI.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IFrankfurterApiClient _frankfurterClient;
        private static readonly string[] ExcludedCurrencies = { "TRY", "PLN", "THB", "MXN" };

        private readonly int _maxRetryAttempts = 3;
        private readonly TimeSpan _pauseBetweenFailures = TimeSpan.FromSeconds(2);

        private readonly AsyncRetryPolicy _retryPolicy;
        public ExchangeRateService(IFrankfurterApiClient frankfurterClient)
        {
            _frankfurterClient = frankfurterClient;
            _retryPolicy = Policy
           .Handle<Exception>()
           .WaitAndRetryAsync(_maxRetryAttempts, i => _pauseBetweenFailures);
        }

        public async Task<Response<CurrencyExchangeRate>> GetLatestRatesAsync(string baseCurrency)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(() => _frankfurterClient.GetLatestRatesAsync(baseCurrency));
            }
            catch (Exception ex)
            {
                return new Response<CurrencyExchangeRate> { Error = "something went wrong" };
            }
        }

        public async Task<Response<CurrencyExchangeRate>> ConvertCurrencyAsync(CurrencyConversionRequest request)
        {
            try
            {
                if (ExcludedCurrencies.Contains(request.TargetCurrency))
                {
                    return new Response<CurrencyExchangeRate> { Error = "Currency conversion for TRY, PLN, THB, and MXN is not supported." };
                }

                return await _retryPolicy.ExecuteAsync(() => _frankfurterClient.ConvertCurrencyAsync(request));
            }
            catch (Exception ex)
            {
                return new Response<CurrencyExchangeRate> { Error = "something went wrong" };
            }
        }

        public async Task<Response<HistoricalRates>> GetHistoricalRatesAsync(HistoricalRateRequest request)
        {
            try
            {
                return await _retryPolicy.ExecuteAsync(() => _frankfurterClient.GetHistoricalRatesAsync(request));

            }
            catch (Exception ex)
            {
                return new Response<HistoricalRates> { Error = "something went wrong" };
            }
        }


    }
}
