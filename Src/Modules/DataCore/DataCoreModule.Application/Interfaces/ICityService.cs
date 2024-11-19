using DataCoreModule.Application.Interfaces.Base;
using DataCoreModule.Core.Models.Entities;

namespace DataCoreModule.Application.Interfaces;

public interface ICityService : IService<City>
{
    public Task<City> UpsertMonitoringCityByName(string cityName);
    public Task<IList<City>> GetMonitoredCities();
}