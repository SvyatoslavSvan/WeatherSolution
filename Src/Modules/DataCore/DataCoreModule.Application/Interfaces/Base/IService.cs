using DataCoreModule.Core.Models.Entities.Base;

namespace DataCoreModule.Application.Interfaces.Base;

public interface IService<TEntity> where TEntity : Entity
{
    public Task<TEntity?> Create(TEntity value);
    public Task<Guid> Delete(Guid id);
    public Task<TEntity?> Update(TEntity value);
    public Task<TEntity?> GetById(Guid id);
    public Task<IList<TEntity>> GetAll();
    public Task<IList<TEntity>> CreateRange(IList<TEntity> range);
}