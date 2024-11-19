using DataCoreModule.Application.DTO;
using DataCoreModule.Core.Models;

namespace DataCoreModule.Application.Interfaces.EnvironmentalMonitoring.ExternalServices;

public interface IEnvironmentalStateExternalService
{
    public Task<TemperatureAirQualityState> GetByTodayHour(int hour, string cityName);
    public Task<IList<TemperatureAirQualityState>> GetByDateRange(DateRange range, string cityName);
}