namespace DataCoreModule.Infrastructure.Presenters.Interfaces;

public interface IEnvironmentalMonitoringPresenter
{
    public Task BeginCityMonitoring(string name);
    public Task StopCityMonitoring(string name);
    public void ChangeInterval(int days);
}