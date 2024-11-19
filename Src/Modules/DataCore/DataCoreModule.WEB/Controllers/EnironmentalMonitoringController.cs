using DataCoreModule.Infrastructure.Presenters.Interfaces;
using DataCoreModule.WEB.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;

namespace DataCoreModule.WEB.Controllers;

[Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class EnvironmentalMonitoringController : ExceptionHandlingController
{
    private readonly IEnvironmentalMonitoringPresenter _presenter;

    public EnvironmentalMonitoringController(IEnvironmentalMonitoringPresenter presenter)
    {
        _presenter = presenter;
    }

    [HttpPost("BeginCityMonitoring/{cityName}")]
    public async Task<IActionResult> BeginCityMonitoring(string cityName) =>
        await ExecuteWithExceptionHandlingAsync(async () =>
        {
            await _presenter.BeginCityMonitoring(cityName);
            return Ok();
        });

    [HttpPost("StopCityMonitoring/{cityName}")]
    public async Task<IActionResult> StopCityMonitoring(string cityName) =>
        await ExecuteWithExceptionHandlingAsync(async () =>
        {
            await _presenter.StopCityMonitoring(cityName);
            return Ok();
        });
    [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
        Policy = "AllAccessPolicy")]
    [HttpPost("ChangeInterval/{days:int}")]
    public IActionResult ChangeInterval(int days) => ExecuteWithExceptionHandling(() =>
    {
        _presenter.ChangeInterval(days);
        return Ok();
    });
}