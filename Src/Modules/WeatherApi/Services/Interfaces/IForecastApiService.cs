using WeatherForecast.DTO.WeatherForecast;

namespace WeatherForecast.Domain.Services.Interfaces
{
    public interface IForecastApiService<T>
    {
        public Task<T> GetTodayForecast(TimeOnly hour, string cityName);

        public Task<IList<T>> GetForecast(Period forecastPeriod, string cityName);
    }
}
