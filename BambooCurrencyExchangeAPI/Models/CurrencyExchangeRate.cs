namespace BambooCurrencyExchangeAPI.Models
{
    public class CurrencyExchangeRate
    {
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
        public DateTime Date { get; set; }
    }
}
