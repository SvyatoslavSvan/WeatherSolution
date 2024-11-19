using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.DTO.AirQualityForecast;
using WeatherForecast.Services.Implementations.Base;
using WeatherForecast.Services.Interfaces;
using WeatherForecast.DTO.WeatherForecast;
using Hourly = WeatherForecast.DTO.AirQualityForecast.Hourly;

namespace WeatherForecast.Services.Implementations.Forecast
{
    public sealed class AirQualityExternalApiService : ForecastService, IForecastApiService<AirQuality>
    {
        private const string HourlyParameters = "european_aqi_pm2_5,european_aqi_pm10,european_aqi_nitrogen_dioxide,european_aqi_ozone,european_aqi_sulphur_dioxide";

        public AirQualityExternalApiService(HttpClient httpClient, IGeoCodingApiService geoCodingApiService) : base(httpClient, geoCodingApiService)
        {
        }

        public async Task<AirQuality> GetTodayForecast(TimeOnly hour, string cityName)
        {
            var builder = GetUriBuilder();
            var city = (await _geoCodingService.GetCitiesByName(cityName)).First();
            CreateRequest(city, builder, collection =>
            {
                collection["hourly"] = HourlyParameters;
            }, isToday: true);
            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);
            var airQualityResponse = await response.Content.ReadFromJsonAsync<AirQualityResponse>();
            if (airQualityResponse is null) 
                throw new NullReferenceException("Forecast response is null");
            var index = airQualityResponse.Hourly.time.FindIndex(x => x.TimeOfDay.Hours == hour.ToTimeSpan().Hours);
            if (index == -1)
                throw new IndexOutOfRangeException("The specified hour was not found in the forecast.");
            return airQualityResponse.Hourly.CreateAirQuality(cityName, index);
        }

        public async Task<IList<AirQuality>> GetForecast(Period forecastPeriod, string cityName)
        {
            var builder = GetUriBuilder();
            var city = (await _geoCodingService.GetCitiesByName(cityName)).First();
            CreateRequest(city, builder, collection =>
            {
                collection["hourly"] = HourlyParameters;
            }, forecastPeriod);
            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);
            var airQualityResponse = await response.Content.ReadFromJsonAsync<AirQualityResponse>() ??
                           throw new NullReferenceException("Forecast response is null");
            return airQualityResponse.Hourly.ToAirQualityCollection(cityName);
        }

        private static UriBuilder GetUriBuilder()
        {
            return new UriBuilder { Scheme = "https", Host = "air-quality-api.open-meteo.com/v1/air-quality" };
        }
    }
}
