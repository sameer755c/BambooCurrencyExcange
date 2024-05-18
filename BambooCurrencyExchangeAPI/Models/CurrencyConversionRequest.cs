namespace BambooCurrencyExchangeAPI.Models
{

    public class CurrencyConversionRequest
    {
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}
