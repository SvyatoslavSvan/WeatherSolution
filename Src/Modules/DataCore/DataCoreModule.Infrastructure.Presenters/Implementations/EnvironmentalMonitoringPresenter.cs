using DataCoreModule.Application.Services;
using DataCoreModule.Infrastructure.Presenters.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataCoreModule.Infrastructure.Presenters.Implementations;

public class EnvironmentalMonitoringPresenter : IEnvironmentalMonitoringPresenter
{
    private readonly EnvironmentalMonitoringService _environmentalMonitoringService;

    public EnvironmentalMonitoringPresenter(IServiceScopeFactory factory)
    {
        _environmentalMonitoringService = factory.CreateScope().ServiceProvider
            .GetServices<IHostedService>()
            .OfType<EnvironmentalMonitoringService>()
            .Single();
    }

    public async Task BeginCityMonitoring(string name) => await _environmentalMonitoringService.AddCity(name);

    public async Task StopCityMonitoring(string name) => await _environmentalMonitoringService.RemoveCity(name);

    public void ChangeInterval(int days) => _environmentalMonitoringService.ChangeIntervalDays(days);
}