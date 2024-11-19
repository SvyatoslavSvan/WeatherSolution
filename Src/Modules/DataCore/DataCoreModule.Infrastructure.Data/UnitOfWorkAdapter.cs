using Calabonga.UnitOfWork;
using DataCoreModule.Application.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace DataCoreModule.Infrastructure.Data;

public class UnitOfWorkAdapter<TContext> : IUnitOfWorkAdapter where TContext : DbContext, IApplicationDbContext //cron
{
    private readonly IUnitOfWork<TContext> _unitOfWork;

    public UnitOfWorkAdapter(IUnitOfWork<TContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IRepository<T> GetRepository<T>() where T : class => _unitOfWork.GetRepository<T>();

    public async Task<int> SaveChangesAsync() => await _unitOfWork.SaveChangesAsync();
}