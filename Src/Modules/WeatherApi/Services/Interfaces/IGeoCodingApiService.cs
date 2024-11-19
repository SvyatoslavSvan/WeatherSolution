using WeatherForecast.DTO.SearchCity;

namespace WeatherForecast.Services.Interfaces
{
    public interface IGeoCodingApiService
    {
        public Task<IList<City>> GetCitiesByName(string name, int count = 1);
    }
}
