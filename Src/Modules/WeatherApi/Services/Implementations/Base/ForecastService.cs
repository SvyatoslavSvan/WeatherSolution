using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using WeatherForecast.DTO.SearchCity;
using WeatherForecast.DTO.WeatherForecast;
using WeatherForecast.Services.Interfaces;

namespace WeatherForecast.Services.Implementations.Base
{
    public abstract class ForecastService : ExternalApiService
    {
        protected readonly IGeoCodingApiService _geoCodingService;

        protected ForecastService(HttpClient httpClient, IGeoCodingApiService geoCodingApiService) : base(httpClient)
        {
            _geoCodingService = geoCodingApiService;
        }

        protected void CreateRequest(City city, UriBuilder builder,
            Action<NameValueCollection> setSpecificParameters, Period? forecastPeriod = null,
            bool isToday = false)
        {
            var query = CreateCityQuery(city);

            setSpecificParameters(query);

            if (isToday)
            {
                query["forecast_days"] = "1";
            }
            else if (forecastPeriod != null)
            {
                SetPeriodToQuery(forecastPeriod, query);
            }

            builder.Query = query.ToString();
        }


        protected static NameValueCollection CreateCityQuery(City city)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[nameof(city.Latitude).ToLower()] = city.Latitude.ToString(CultureInfo.InvariantCulture);
            query[nameof(city.Longitude).ToLower()] = city.Longitude.ToString(CultureInfo.InvariantCulture);
            return query;
        }

        private static void SetPeriodToQuery(Period forecastPeriod, NameValueCollection query)
        {
            query["start_date"] = forecastPeriod.From.Date.ToString("yyyy-MM-dd");
            query["end_date"] = forecastPeriod.To.Date.ToString("yyyy-MM-dd");
        }
    }
}
