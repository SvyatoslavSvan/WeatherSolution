using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.DTO.WeatherForecast;

namespace WeatherForecast.Controllers.Base
{
    public abstract class ForecastController<T> : ExceptionHandlingController
    {
        private readonly IForecastApiService<T> _forecastApiService;

        private protected ForecastController(IForecastApiService<T> forecastApiService)
        {
            _forecastApiService = forecastApiService;
        }

        [HttpGet("TodayForecast")]
        public async Task<IActionResult> TodayForecast(int hour, string cityName) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _forecastApiService.GetTodayForecast(new TimeOnly(hour, 00), cityName)));

        [HttpGet("Forecast")]
        public async Task<IActionResult> Forecast(DateTime from, DateTime to, string cityName) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _forecastApiService.GetForecast(new Period(from, to), cityName)));

        
    }
}
