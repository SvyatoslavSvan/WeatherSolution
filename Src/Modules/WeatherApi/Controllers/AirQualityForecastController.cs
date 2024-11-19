using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Controllers.Base;
using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.DTO.AirQualityForecast;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AirQualityForecastController : ForecastController<AirQuality>
    {
        public AirQualityForecastController(IForecastApiService<AirQuality> forecastApiService) : base(forecastApiService)
        {
        }
    }
}
