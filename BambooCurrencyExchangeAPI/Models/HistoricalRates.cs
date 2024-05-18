namespace BambooCurrencyExchangeAPI.Models
{
    public class HistoricalRates
    {
        public double amount { get; set; }
        public string @base { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public Dictionary<string, Dictionary<string, decimal>>? Rates { get; set; }
    }
}
