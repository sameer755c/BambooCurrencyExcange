using BambooCurrencyExchangeAPI.Controllers;
using BambooCurrencyExchangeAPI.Helpers;
using BambooCurrencyExchangeAPI.Models;
using BambooCurrencyExchangeAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BambooCardAPITests
{
    public class ExchangeRateClientServiceTests
    {
        private Mock<IFrankfurterApiClient> _mockClient;
        private ExchangeRateService _service;

        [SetUp]
        public void Setup()
        {
            _mockClient = new Mock<IFrankfurterApiClient>();         
            _service = new ExchangeRateService(_mockClient.Object);
        }

        [Test]
        public async Task GetLatestRatesAsync_ReturnsRates()
        {
            // Arrange
            var exchangeRate = new CurrencyExchangeRate { Base = "EUR", Rates = new Dictionary<string, decimal> { { "USD", 1.1m } }, Date = System.DateTime.Now };
            _mockClient.Setup(client => client.GetLatestRatesAsync("EUR"))
                .ReturnsAsync(new Response<CurrencyExchangeRate> { Data = exchangeRate });

            // Act
            var result = await _service.GetLatestRatesAsync("EUR");

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("EUR", result.Data.Base);
        }

        [Test]
        public async Task ConvertCurrencyAsync_Currency_Returns()
        {
            // Arrange
            var request = new CurrencyConversionRequest { BaseCurrency = "EUR", TargetCurrency = "USD", Amount = 100 };

            // Act
            var result = await _service.ConvertCurrencyAsync(request);

            // Assert
            Assert.IsNull(result);  // no error faced 
           
        }

        [Test]
        public async Task GetHistoricalRatesAsync_ReturnsRates()
        {
            // Arrange
            var historicalRates = new HistoricalRates() { amount = 100, start_date =DateTime.Now.ToShortDateString()
                                                        , end_date = DateTime.Now.AddDays(1).ToShortDateString()                                                     
                                                            };
         
            var request = new HistoricalRateRequest (){ StartDate = new System.DateTime(2020, 1, 1), EndDate = new System.DateTime(2020, 1, 31)};
            _mockClient.Setup(client => client.GetHistoricalRatesAsync(request))
                .ReturnsAsync(new Response<HistoricalRates> { Data = historicalRates });

            // Act
            var result = await _service.GetHistoricalRatesAsync(request);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
    
        }
    }
}
