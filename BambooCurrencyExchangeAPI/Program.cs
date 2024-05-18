using BambooCurrencyExchangeAPI.Helpers;
using BambooCurrencyExchangeAPI.Middlewares;
using BambooCurrencyExchangeAPI.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddHttpClient<IFrankfurterApiClient, FrankfurterApiClient>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExchangeRateLimitingMiddleWare>();
app.UseAuthorization();

app.MapControllers();

app.Run();
