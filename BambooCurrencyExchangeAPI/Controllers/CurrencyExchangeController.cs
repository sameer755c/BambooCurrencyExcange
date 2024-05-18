using BambooCurrencyExchangeAPI.Models;
using BambooCurrencyExchangeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BambooCurrencyExchangeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public CurrencyExchangeController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestRates([FromQuery] string baseCurrency)
        {
            var result = await _exchangeRateService.GetLatestRatesAsync(baseCurrency);
            if (!result.Success) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("convert")]
        public async Task<IActionResult> ConvertCurrency([FromQuery] CurrencyConversionRequest request)
        {
            var result = await _exchangeRateService.ConvertCurrencyAsync(request);
            if (!result.Success) return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpGet("historical")]
        public async Task<IActionResult> GetHistoricalRates([FromQuery] HistoricalRateRequest request)
        {
            var result = await _exchangeRateService.GetHistoricalRatesAsync(request);
            if (!result.Success) return BadRequest(result.Error);

            return Ok(result.Data);
        }
    }
}
