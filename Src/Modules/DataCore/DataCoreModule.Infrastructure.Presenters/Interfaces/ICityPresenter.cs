using DataCoreModule.Core.Models.Entities;
using DataCoreModule.Infrastructure.Presenters.BindingModels.City.Base;
using DataCoreModule.Infrastructure.Presenters.Interfaces.Base;

namespace DataCoreModule.Infrastructure.Presenters.Interfaces;

public interface ICityPresenter : IPresenter<City, WritableCityBindingModel, WritableCityBindingModel>
{
       
}