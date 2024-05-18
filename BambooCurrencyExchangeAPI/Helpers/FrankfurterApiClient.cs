using BambooCurrencyExchangeAPI.Models;
using static System.Net.WebRequestMethods;

namespace BambooCurrencyExchangeAPI.Helpers
{
    public class FrankfurterApiClient : IFrankfurterApiClient
    {
        private readonly HttpClient _httpClient;
        private const string _basaeUrl=$"https://api.frankfurter.app";

        public FrankfurterApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_basaeUrl);
        }

        public async Task<Response<CurrencyExchangeRate>> GetLatestRatesAsync(string baseCurrency)
        {
            var response = await _httpClient.GetAsync($"/latest?base={baseCurrency}");
            if (response.IsSuccessStatusCode)
            {
                var rates = await response.Content.ReadFromJsonAsync<CurrencyExchangeRate>();
                return new Response<CurrencyExchangeRate> { Data = rates };
            }
            return new Response<CurrencyExchangeRate> { Error = "Failed to retrieve exchange rates." };
        }

        public async Task<Response<CurrencyExchangeRate>> ConvertCurrencyAsync(CurrencyConversionRequest request)
        {
            var response = await _httpClient.GetAsync($"/latest?from={request.BaseCurrency}&to={request.TargetCurrency}");
            if (response.IsSuccessStatusCode)
            {
                var rates = await response.Content.ReadFromJsonAsync<CurrencyExchangeRate>();
                rates.Rates = rates.Rates.Where(r => r.Key == request.TargetCurrency.ToUpper()).ToDictionary(r => r.Key, r => r.Value * request.Amount);
                return new Response<CurrencyExchangeRate> { Data = rates };
            }
            return new Response<CurrencyExchangeRate> { Error = "Failed to convert currency." };
        }

        public async Task<Response<HistoricalRates>> GetHistoricalRatesAsync(HistoricalRateRequest request)
        {
            var response = await _httpClient.GetAsync($"/{request.StartDate:yyyy-MM-dd}..{request.EndDate:yyyy-MM-dd}");
            if (response.IsSuccessStatusCode)
            {              
                var rates = await response.Content.ReadFromJsonAsync<HistoricalRates>();
                return new Response<HistoricalRates> { Data = rates };
            }
            return new Response<HistoricalRates> { Error = "Failed to retrieve historical rates." };
        }
    }
}
