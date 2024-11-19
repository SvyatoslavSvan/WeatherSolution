using DataCoreModule.Core.Models.Entities.Base;

namespace DataCoreModule.Infrastructure.Presenters.Interfaces.Base;

public interface IPresenter<TEntity, in TCreateBindingModel, in TUpdateBindingModel> where TEntity : Entity
{
    public Task<TEntity> Create(TCreateBindingModel value);
    public Task<Guid> Delete(Guid id);
    public Task<TEntity> Update(TUpdateBindingModel value);
    public Task<TEntity> GetById(Guid id);
    public Task<IList<TEntity>> GetAll();
}