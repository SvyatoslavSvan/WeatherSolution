using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Web;
using AutoMapper;
using DataCoreModule.Application.DTO;
using DataCoreModule.Application.Interfaces.EnvironmentalMonitoring.ExternalServices;
using DataCoreModule.Core.Models;
using WeatherSystem.Shared.DTO.WeatherForecast;
using AirQuality = WeatherSystem.Shared.DTO.AirQualityForecast.AirQuality;

namespace DataCoreModule.Infrastructure.ExternalServices.Implementations;

public class EnvironmentalStateExternalService(HttpClient httpClient, IMapper mapper) : IEnvironmentalStateExternalService
{
    public async Task<TemperatureAirQualityState> GetByTodayHour(int hour, string cityName)
    {
        var uriBuilder = new UriBuilder(httpClient.BaseAddress!)
        {
            Query = GetTodayHourQuery(hour, cityName).ToString()
        };
        var temperature = await httpClient.GetFromJsonAsync<TemperatureState>(uriBuilder.ToString());
        var airQuality = await httpClient.GetFromJsonAsync<AirQuality>(uriBuilder.ToString());
        return mapper.Map<TemperatureAirQualityState>((temperature, airQuality));
    }

    public async Task<IList<TemperatureAirQualityState>> GetByDateRange(DateRange range, string cityName)
    {
        var airQualities = await httpClient.GetFromJsonAsync<IList<AirQuality>>(GetUriBuilder(range, cityName, "api/AirQualityForecast/Forecast").ToString());
        var temperatures = await httpClient.GetFromJsonAsync<IList<TemperatureState>>(GetUriBuilder(range, cityName, "api/TemperatureStateForecast/Forecast").ToString());
        var result = new TemperatureAirQualityState[temperatures!.Count];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = mapper.Map<TemperatureAirQualityState>((temperatures[i], airQualities![i]));
        }
        return result.ToList();
    }

    private UriBuilder GetUriBuilder(DateRange range, string cityName, string path)
        => new(httpClient.BaseAddress!)
        {
            Path = path,
            Query = GetDateRangeQuery(range, cityName).ToString()
        };

    private static NameValueCollection GetDateRangeQuery(DateRange range, string cityName)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["from"] = range.Start.ToString("O");
        query["to"] = range.End.ToString("O");
        query["cityName"] = cityName;
        return query;
    }

    private static NameValueCollection GetTodayHourQuery(int hour, string cityName)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["hour"] = hour.ToString();
        query["cityName"] = cityName;
        return query;
    }
}