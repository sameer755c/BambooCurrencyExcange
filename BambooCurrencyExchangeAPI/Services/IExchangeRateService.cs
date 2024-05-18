using BambooCurrencyExchangeAPI.Models;

namespace BambooCurrencyExchangeAPI.Services
{
    public interface IExchangeRateService
    {
        Task<Response<CurrencyExchangeRate>> GetLatestRatesAsync(string baseCurrency);
        Task<Response<CurrencyExchangeRate>> ConvertCurrencyAsync(CurrencyConversionRequest request);
        Task<Response<HistoricalRates>> GetHistoricalRatesAsync(HistoricalRateRequest request);
    }
}
