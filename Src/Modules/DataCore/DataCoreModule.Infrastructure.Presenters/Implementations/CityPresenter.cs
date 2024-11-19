using AutoMapper;
using DataCoreModule.Application.Interfaces.Base;
using DataCoreModule.Core.Models.Entities;
using DataCoreModule.Infrastructure.Presenters.BindingModels.City.Base;
using DataCoreModule.Infrastructure.Presenters.Implementations.Base;
using DataCoreModule.Infrastructure.Presenters.Interfaces;

namespace DataCoreModule.Infrastructure.Presenters.Implementations;

public class CityPresenter : Presenter<City, WritableCityBindingModel, WritableCityBindingModel>, ICityPresenter
{
    public CityPresenter(IMapper mapper, IService<City> service)
        : base(mapper, service)  {  }
}