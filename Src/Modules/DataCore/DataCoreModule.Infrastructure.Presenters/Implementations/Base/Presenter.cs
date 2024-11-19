using AutoMapper;
using DataCoreModule.Application.Interfaces.Base;
using DataCoreModule.Core.Models.Entities.Base;
using DataCoreModule.Infrastructure.Presenters.Interfaces.Base;

namespace DataCoreModule.Infrastructure.Presenters.Implementations.Base;

public class Presenter<TEntity, TCreateBindingModel, TUpdateBindingModel> : IPresenter<TEntity, TCreateBindingModel, TUpdateBindingModel> where TEntity : Entity
{

    protected const string NullReferenceExceptionMessage =
        $"Value from service was null in Presenter<{nameof(TEntity)}> was null";

    protected readonly IService<TEntity> Service;
    protected readonly IMapper Mapper;

    public Presenter(IMapper mapper, IService<TEntity> service)
    {
        Mapper = mapper;
        Service = service;
    }

    public virtual async Task<TEntity> Create(TCreateBindingModel value)
    {
        return await Service.Create(Mapper.Map<TCreateBindingModel, TEntity>(value)) ??
               throw new NullReferenceException(NullReferenceExceptionMessage);
    }

    public virtual async Task<Guid> Delete(Guid id) => await Service.Delete(id);

    public virtual async Task<TEntity> Update(TUpdateBindingModel value) =>
        await Service.Update(Mapper.Map<TUpdateBindingModel, TEntity>(value)) ??
        throw new NullReferenceException(NullReferenceExceptionMessage);


    public async Task<TEntity> GetById(Guid id) =>
        await Service.GetById(id) ?? throw new NullReferenceException(NullReferenceExceptionMessage);

    public async Task<IList<TEntity>> GetAll() =>
        await Service.GetAll() ?? throw new NullReferenceException(NullReferenceExceptionMessage);
}