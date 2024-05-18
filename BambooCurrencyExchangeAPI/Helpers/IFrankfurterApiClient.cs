using BambooCurrencyExchangeAPI.Models;

namespace BambooCurrencyExchangeAPI.Helpers
{
    public interface IFrankfurterApiClient
    {
        Task<Response<CurrencyExchangeRate>> GetLatestRatesAsync(string baseCurrency);
        Task<Response<CurrencyExchangeRate>> ConvertCurrencyAsync(CurrencyConversionRequest request);
        Task<Response<HistoricalRates>> GetHistoricalRatesAsync(HistoricalRateRequest request);
    }
}
