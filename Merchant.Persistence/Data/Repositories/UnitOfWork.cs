using Merchant.Domain.Base;
using Merchant.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Merchant.Persistence.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly EfContext _dbContext;

    public UnitOfWork(EfContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IRepositoryBase<T> Repository<T, TId>()
        where T : BaseEntity<TId>
        where TId : struct

    {
        return new RepositoryBase<T, TId>(_dbContext);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    public int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return _dbContext.SaveChanges(acceptAllChangesOnSuccess);
    }

    public async Task Rollback()
    {
        await Task.Run(() => _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload()));
    }
}