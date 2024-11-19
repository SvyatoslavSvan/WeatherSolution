using AutoMapper;
using DataCoreModule.Application.Interfaces;
using DataCoreModule.Application.Interfaces.Data;
using DataCoreModule.Application.Services.Base;
using DataCoreModule.Core.Models.Entities;

namespace DataCoreModule.Application.Services;

public sealed class EnvironmentalService(IUnitOfWorkAdapter unitOfWork, IMapper mapper, ICityService cityService)
    : Service<EnvironmentalState>(unitOfWork, mapper), IEnvironmentalService
{
    public override async Task<IList<EnvironmentalState>> CreateRange(IList<EnvironmentalState> range)
    {
        await SetAttachCity(range);
        return await base.CreateRange(range);
    }

    private async Task SetAttachCity(IList<EnvironmentalState> range)
    {
        var existingCity = await cityService.GetById(range.First().City.Id);
        if (existingCity != null)
        {
            foreach (var item in range)
            {
                item.City = existingCity;
            }
        }
    }
}