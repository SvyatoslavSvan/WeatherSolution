using System.Web;
using WeatherForecast.DTO.SearchCity;
using WeatherForecast.Services.Implementations.Base;
using WeatherForecast.Services.Interfaces;

namespace WeatherForecast.Services.Implementations.GeoCoding
{
    public sealed class GeoCodingExternalApiService : ExternalApiService, IGeoCodingApiService
    {

        public GeoCodingExternalApiService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IList<City>> GetCitiesByName(string name, int count = 1)
        {
            UriBuilder builder = new()
            {
                Scheme = "https",
                Host = "geocoding-api.open-meteo.com/v1/search"
            };

            CreateCityRequest(name, count, builder);

            var response = await _httpClient.GetAsync(builder.Uri);
            ProceedResponse(response);

            var cities = await response.Content.ReadFromJsonAsync<CityRequest>()
                         ?? throw new NullReferenceException("City response is null");

            return cities.ToCityCollection();
        }

        private void CreateCityRequest(string name, int count, UriBuilder builder)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["name"] = name;
            query["count"] = count.ToString();
            query["language"] = "en";
            query["format"] = "json";
            builder.Query = query.ToString();
        }
    }
}
