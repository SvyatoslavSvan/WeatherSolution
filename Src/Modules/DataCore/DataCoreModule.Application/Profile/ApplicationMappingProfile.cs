using DataCoreModule.Application.DTO;
using DataCoreModule.Core.Models;
using DataCoreModule.Core.Models.Entities;

namespace DataCoreModule.Application.Profile;

public class ApplicationMappingProfile : AutoMapper.Profile
{
    public const string cityKey = "city";
    public ApplicationMappingProfile()
    {
        CreateMap<TemperatureAirQualityState, EnvironmentalState>().ConstructUsing((temperatureQuality, context) =>
        {
            var city = context.Items[cityKey] as City;
            return EnvironmentalState.CreateInstance(city!, temperatureQuality.Temperature,
                new AirQuality(temperatureQuality.EuropeanAirQualityNitrogenDioxide,
                    temperatureQuality.EuropeanAirQualityPm10, temperatureQuality.EuropeanAirQualityPm25,
                    temperatureQuality.EuropeanAirQualityOzone,
                    temperatureQuality.EuropeanAirQualitySulphurDioxide), temperatureQuality.Time);
        });
    }
}