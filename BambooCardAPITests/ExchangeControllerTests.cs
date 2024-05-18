using BambooCurrencyExchangeAPI.Controllers;
using BambooCurrencyExchangeAPI.Models;
using BambooCurrencyExchangeAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BambooCardAPITests
{
    public class ExchangeControllerTests
    {
        private Mock<IExchangeRateService> _mockService;
        private CurrencyExchangeController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IExchangeRateService>();
            _controller = new CurrencyExchangeController(_mockService.Object);
        }

        [Test]
        public async Task GetLatestRates_ReturnsOkResult()
        {
            // Arrange
            var exchangeRate = new CurrencyExchangeRate { Base = "EUR", Rates = new Dictionary<string, decimal> { { "USD", 500m } } };
            _mockService.Setup(service => service.GetLatestRatesAsync("EUR")).ReturnsAsync(new Response<CurrencyExchangeRate> { Data = exchangeRate });

            // Act
            var result = await _controller.GetLatestRates("EUR");

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as CurrencyExchangeRate;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual("EUR", returnValue.Base);
        }


        [Test]
        public async Task ConvertCurrency_UnsupportedCurrency_ReturnsBadRequest()
        {
            // Arrange
            var request = new CurrencyConversionRequest { BaseCurrency = "EUR", TargetCurrency = "TRY", Amount = 100 };
            _mockService.Setup(service => service.ConvertCurrencyAsync(request))
                .ReturnsAsync(new Response<CurrencyExchangeRate> { Error = "TRY, PLN, THB, and MXN currencies conversion is not allowed." });

            // Act
            var result = await _controller.ConvertCurrency(request);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("TRY, PLN, THB, and MXN currencies conversion is not allowed.", badRequestResult.Value);
        }
    }
}