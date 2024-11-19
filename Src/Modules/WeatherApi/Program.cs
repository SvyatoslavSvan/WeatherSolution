using WeatherForecast.Domain.Services;
using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.DTO.AirQualityForecast;
using WeatherForecast.DTO.WeatherForecast;
using WeatherForecast.Services.Implementations.Forecast;
using WeatherForecast.Services.Implementations.GeoCoding;
using WeatherForecast.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IForecastApiService<AirQuality>, AirQualityExternalApiService>();
builder.Services.AddScoped<IForecastApiService<TemperatureState>, TemperatureStateExternalApiService>();
builder.Services.AddScoped<IGeoCodingApiService, GeoCodingExternalApiService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
