using System.Collections.Concurrent;
using AutoMapper;
using DataCoreModule.Application.DTO;
using DataCoreModule.Application.Interfaces;
using DataCoreModule.Application.Interfaces.Base;
using DataCoreModule.Application.Interfaces.EnvironmentalMonitoring.ExternalServices;
using DataCoreModule.Application.Profile;
using DataCoreModule.Core.Models;
using DataCoreModule.Core.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataCoreModule.Application.Services;

public sealed class EnvironmentalMonitoringService(IServiceScopeFactory factory) : BackgroundService
{
    private const int DefaultIntervalDays = 1;
    private readonly ConcurrentDictionary<Guid, City> _cities = [];
    private readonly object _lock = new();
    private TimeSpan _interval = TimeSpan.FromDays(DefaultIntervalDays);

    public IEnumerable<City> Cities => _cities.Values.ToList();

    public async Task AddCity(string cityName)
    {
        using var scope = factory.CreateScope();
        var cityService = scope.ServiceProvider.GetRequiredService<ICityService>();
        var city = await cityService.UpsertMonitoringCityByName(cityName);
        if (!_cities.TryAdd(city.Id, city))
            throw new InvalidOperationException("Cannot add monitoring for existing city");
    }

    public async Task RemoveCity(string cityName)
    {
        var cityToRemove = _cities.Values.FirstOrDefault(x => x.Name == cityName);
        if (cityToRemove is null || !_cities.TryRemove(cityToRemove.Id, out _))
            throw new InvalidOperationException("Cannot remove non-monitored city");
        await UpdateIsMonitoredForCity(cityToRemove);
    }


    public void ChangeIntervalDays(int days)
    {
        if (days >= DefaultIntervalDays)
            _interval = TimeSpan.FromDays(days);
        else
            throw new ArgumentException($"Interval Days cannot be lower than {DefaultIntervalDays}");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await AddExistingMonitoringCities();
        var lastDelayInterval = _interval;
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = factory.CreateScope();
            var context = new ServiceContext(scope.ServiceProvider);
            await WaitForCitiesIsNotEmpty(stoppingToken);
            GetDateRangeForForecast(out var dateRange);
            var citiesSnapshot = _cities.Values.ToList();
            if (lastDelayInterval != _interval)
            {
                await Task.Delay(_interval - lastDelayInterval, stoppingToken);
            }
            else
            {
                lastDelayInterval = _interval;
                await InsertForecastForMonitoredCities(citiesSnapshot, dateRange, context);
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }

    private async Task UpdateIsMonitoredForCity(City city)
    {
        using var scope = factory.CreateScope();
        var cityService = scope.ServiceProvider.GetRequiredService<ICityService>();
        city.IsMonitored = false;
        await cityService.Update(city);
    }

    private async Task AddExistingMonitoringCities()
    {
        using var scope = factory.CreateScope();
        var monitoredCities = await scope.ServiceProvider
            .GetRequiredService<ICityService>()
            .GetMonitoredCities();

        foreach (var city in monitoredCities) _cities.TryAdd(city.Id, city);
    }

    private async Task InsertForecastForMonitoredCities(List<City> citiesSnapshot, DateRange dateRange,
        ServiceContext serviceContext)
    {
        foreach (var city in citiesSnapshot) await ProcessForecastForCity(dateRange, city, serviceContext);
    }

    private void GetDateRangeForForecast(out DateRange range)
    {
        range = new DateRange
        {
            Start = DateTime.Today.Date,
            End = DateTime.Today.Date.AddDays(_interval.Days - 1)
        };
    }

    private static async Task ProcessForecastForCity(DateRange range, City city, ServiceContext serviceContext)
    {
        var forecastForCity = await GetForecastForCity(range, city, serviceContext);
        await InsertForecastForCity(forecastForCity, city, serviceContext);
    }

    private async Task WaitForCitiesIsNotEmpty(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            lock (_lock)
            {
                if (_cities.Count is not 0) break;
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private static async Task<IList<TemperatureAirQualityState>> GetForecastForCity(DateRange range, City city,
        ServiceContext serviceContext)
    {
        return await serviceContext.ExternalService.GetByDateRange(range, city.Name);
    }

    private static async Task InsertForecastForCity(IList<TemperatureAirQualityState> forecastForCity, City city,
        ServiceContext serviceContext)
    {
        await serviceContext.EnvironmentalService.CreateRange(serviceContext.Mapper.Map<IList<EnvironmentalState>>(
            forecastForCity,
            opts => { opts.Items[ApplicationMappingProfile.cityKey] = city; }));
    }


    private class ServiceContext(IServiceProvider serviceProvider)
    {
        public IEnvironmentalStateExternalService ExternalService { get; } =
            serviceProvider.GetRequiredService<IEnvironmentalStateExternalService>();

        public IService<EnvironmentalState> EnvironmentalService { get; } =
            serviceProvider.GetRequiredService<IEnvironmentalService>();

        public IMapper Mapper { get; } = serviceProvider.GetRequiredService<IMapper>();
    }
}