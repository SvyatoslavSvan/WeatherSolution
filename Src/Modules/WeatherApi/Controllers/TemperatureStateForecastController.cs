using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Controllers.Base;
using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.DTO.WeatherForecast;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class TemperatureStateForecastController : ForecastController<TemperatureState>
    {
        public TemperatureStateForecastController(IForecastApiService<TemperatureState> forecastApiService) : base(forecastApiService)
        {
        }
    }
}
