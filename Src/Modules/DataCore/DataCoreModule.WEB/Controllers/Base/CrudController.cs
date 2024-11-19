using DataCoreModule.Core.Models.Entities.Base;
using DataCoreModule.Infrastructure.Presenters.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;

namespace DataCoreModule.WEB.Controllers.Base;

public abstract class CrudController<TEntity, TCreateBindingModel, TUpdateBindingModel> : ExceptionHandlingController
    where TEntity : Entity 
{
    protected readonly IPresenter<TEntity, TCreateBindingModel, TUpdateBindingModel> Presenter;

    protected CrudController(IPresenter<TEntity, TCreateBindingModel, TUpdateBindingModel> presenter)
    {
        Presenter = presenter;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll() =>
        await ExecuteWithExceptionHandlingAsync(async () =>
            Ok(await Presenter.GetAll()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) =>
        await ExecuteWithExceptionHandlingAsync(async () =>
            Ok(await Presenter.GetById(id)));

    [HttpPost]
    public async Task<IActionResult> Create(TCreateBindingModel model) =>
        await ExecuteWithExceptionHandlingAsync(async () =>
            Ok(await Presenter.Create(model)));

    [HttpPut]
    public async Task<IActionResult> Update(TUpdateBindingModel model) =>
        await ExecuteWithExceptionHandlingAsync(async () =>
            Ok(await Presenter.Update(model)));
}