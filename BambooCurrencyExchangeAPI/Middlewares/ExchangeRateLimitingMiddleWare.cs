
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BambooCurrencyExchangeAPI.Middlewares
{
    public class ExchangeRateLimitingMiddleWare
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, int> _requestCounts = new ConcurrentDictionary<string, int>();
        private static readonly int _requestLimit = 200;
        private static readonly TimeSpan _resetPeriod = TimeSpan.FromMinutes(1);

        public ExchangeRateLimitingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            _requestCounts.AddOrUpdate(ipAddress, 1, (key, count) => count + 1);

            if (_requestCounts[ipAddress] > _requestLimit)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync(" limit exceeded. Please try again later.");
            }
            else
            {
                await _next(context);
            }

            // Reset the count after the period
            await Task.Delay(_resetPeriod).ContinueWith(_ => _requestCounts[ipAddress] = 0);
        }
    }
}
