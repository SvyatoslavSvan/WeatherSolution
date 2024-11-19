using DataCoreModule.Core.Models.Entities;
using DataCoreModule.Infrastructure.Presenters.BindingModels.City.Base;
using DataCoreModule.Infrastructure.Presenters.Interfaces.Base;
using DataCoreModule.WEB.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DataCoreModule.WEB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController : CrudController<City,WritableCityBindingModel, WritableCityBindingModel>
{
    public CityController(IPresenter<City, WritableCityBindingModel, WritableCityBindingModel> presenter) : base(presenter)
    {
    }
}