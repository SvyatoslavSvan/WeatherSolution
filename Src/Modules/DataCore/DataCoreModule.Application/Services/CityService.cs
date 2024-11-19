using AutoMapper;
using DataCoreModule.Application.Interfaces;
using DataCoreModule.Application.Interfaces.Data;
using DataCoreModule.Application.Services.Base;
using DataCoreModule.Core.Models.Entities;

namespace DataCoreModule.Application.Services;

public sealed class CityService(IUnitOfWorkAdapter unitOfWork, IMapper mapper)
    : Service<City>(unitOfWork, mapper), ICityService
{
    public override async Task<City?> Create(City value)
    {
        if (await Repository.ExistsAsync(x => x.Name == value.Name)) return value;
        await Repository.InsertAsync(value);
        await UnitOfWork.SaveChangesAsync();
        return value;
    }

    public async Task<City> UpsertMonitoringCityByName(string cityName)
    {
        var city = await Repository.GetFirstOrDefaultAsync(predicate: x => x.Name == cityName);
        if (city == null)
        {
            city = new City(cityName, true);
            await Repository.InsertAsync(city);
        }
        else
        {
            city.IsMonitored = true;
            Repository.Update(city);
        }
        await UnitOfWork.SaveChangesAsync();
        return city;
    }

    public async Task<IList<City>> GetMonitoredCities() => await Repository.GetAllAsync(predicate: x => x.IsMonitored == true, disableTracking: false);



}