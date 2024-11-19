using Calabonga.UnitOfWork;

namespace DataCoreModule.Application.Interfaces.Data;

public interface IUnitOfWorkAdapter 
{
    public IRepository<T> GetRepository<T>() where T : class;
    Task<int> SaveChangesAsync();
}