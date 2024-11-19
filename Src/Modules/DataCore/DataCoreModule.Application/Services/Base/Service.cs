using AutoMapper;
using Calabonga.UnitOfWork;
using DataCoreModule.Application.Interfaces.Base;
using DataCoreModule.Application.Interfaces.Data;
using DataCoreModule.Core.Models.Entities.Base;

namespace DataCoreModule.Application.Services.Base;

public class Service<TEntity> : IService<TEntity> where TEntity : Entity
{
    protected readonly IUnitOfWorkAdapter UnitOfWork;
    protected readonly IRepository<TEntity> Repository;

    public Service(IUnitOfWorkAdapter unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Repository = UnitOfWork.GetRepository<TEntity>();
    }

    public virtual async Task<TEntity?> Create(TEntity value)
    { 
        await Repository.InsertAsync(value);
        await UnitOfWork.SaveChangesAsync();
        return value;
    }

    public virtual async Task<Guid> Delete(Guid id)
    {
        Repository.Delete(id);
        await UnitOfWork.SaveChangesAsync();
        return id;
    }

    public virtual async Task<TEntity?> Update(TEntity value)
    {
        Repository.Update(value);
        await UnitOfWork.SaveChangesAsync();
        return value;
    }

    public async Task<TEntity?> GetById(Guid id) => await Repository.FindAsync(id);

    public async Task<IList<TEntity>> GetAll() => (await Repository.GetAllAsync(true))!;

    public virtual async Task<IList<TEntity>> CreateRange(IList<TEntity> range)
    {
        await Repository.InsertAsync(range);
        await UnitOfWork.SaveChangesAsync();
        return range;
    }
}