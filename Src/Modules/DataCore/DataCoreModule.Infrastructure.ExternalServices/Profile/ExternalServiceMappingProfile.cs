using DataCoreModule.Application.DTO;
using WeatherSystem.Shared.DTO.AirQualityForecast;
using WeatherSystem.Shared.DTO.WeatherForecast;

namespace DataCoreModule.Infrastructure.ExternalServices.Profile;

public class ExternalServiceMappingProfile : AutoMapper.Profile
{
    public ExternalServiceMappingProfile()
    {
        CreateMap<(TemperatureState, AirQuality), TemperatureAirQualityState>().ConvertUsing(x => new TemperatureAirQualityState()
        {
            EuropeanAirQualityNitrogenDioxide = x.Item2.EuropeanAirQualityNitrogenDioxide,
            EuropeanAirQualitySulphurDioxide = x.Item2.EuropeanAirQualitySulphurDioxide,
            EuropeanAirQualityOzone = x.Item2.EuropeanAirQualityOzone,
            EuropeanAirQualityPm10 = x.Item2.EuropeanAirQualityPm10,
            EuropeanAirQualityPm25 = x.Item2.EuropeanAirQualityPm25,
            Temperature = x.Item1.Temperature,
            Time = x.Item1.Time,
        }); ;
    }
}