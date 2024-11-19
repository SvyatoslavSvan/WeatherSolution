using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.DTO.SearchCity;
using WeatherForecast.DTO.WeatherForecast;
using WeatherForecast.Services.Implementations.Base;
using WeatherForecast.Services.Interfaces;


namespace WeatherForecast.Services.Implementations.Forecast
{
    public sealed class TemperatureStateExternalApiService : ForecastService, IForecastApiService<TemperatureState>
    {

        public TemperatureStateExternalApiService(HttpClient httpClient, IGeoCodingApiService geoCodingService) : base(httpClient,
            geoCodingService)
        {
        }

        public async Task<TemperatureState> GetTodayForecast(TimeOnly hour, string cityName)
        {
            var builder = GetUriBuilder();
            var city = (await _geoCodingService.GetCitiesByName(cityName)).First();
            CreateRequest(city, builder, collection =>
            {
                collection["temperature_unit"] = "celsius";
                collection["hourly"] = "temperature_2m";
            });
            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);
            var forecast = await response.Content.ReadFromJsonAsync<TemperatureStateResponse>();
            if (forecast is null) throw new NullReferenceException("Forecast response is null");
            var index = forecast.Hourly.Time.FindIndex(x => x.TimeOfDay.Hours == hour.ToTimeSpan().Hours);
            if (index == -1) throw new IndexOutOfRangeException("The specified hour was not found in the forecast.");
            return new TemperatureState(forecast.Hourly.Time[index], forecast.Hourly.Temperature_2m[index], cityName);
        }

        public async Task<IList<TemperatureState>> GetForecast(Period forecastPeriod, string cityName)
        {
            var builder = GetUriBuilder();
            var city = (await _geoCodingService.GetCitiesByName(cityName)).First();
            CreateRequest(city, builder, collection =>
            {
                collection["temperature_unit"] = "celsius";
                collection["hourly"] = "temperature_2m";
            }, forecastPeriod);
            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);
            var forecast = await response.Content.ReadFromJsonAsync<TemperatureStateResponse>() ??
                           throw new NullReferenceException("Forecast response is null");
            return forecast.Hourly.ToTemperatureStateCollection(cityName);
        }

        private static UriBuilder GetUriBuilder()
        {
            return new UriBuilder { Scheme = "https", Host = "api.open-meteo.com/v1/forecast" };
        }


    }

}
