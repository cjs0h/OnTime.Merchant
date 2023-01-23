using System.Linq.Expressions;
using Merchant.Domain.Base;
using Merchant.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Merchant.Persistence.Data.Repositories;

public class RepositoryBase<TModel, TId> : IRepositoryBase<TModel>
    where TModel : BaseEntity<TId>
    where TId : struct
{
    private bool _disposed;
    public DbContext RepositoryContext { get; set; }

    public RepositoryBase(DbContext repositoryContext)
    {
        RepositoryContext = repositoryContext;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
            {
                RepositoryContext.Dispose();
            }

        _disposed = true;
    }

    public IQueryable<TModel> FindAll()
    {
        return RepositoryContext.Set<TModel>().AsNoTracking();
    }

    public IQueryable<TModel> FindByCondition(Expression<Func<TModel, bool>> predicate,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null,
        bool disableTracking = true)
    {
        var query = RepositoryContext.Set<TModel>().Where(predicate);
        if (include != null)
            query = include(query);
        if (orderBy != null)
            query = orderBy(query);
        return disableTracking ? query.AsNoTracking().AsQueryable() : query.AsQueryable();
    }

    public async Task<TModel?> FindItemByCondition(Expression<Func<TModel, bool>> predicate,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null,
        bool disableTracking = true)
    {
        IQueryable<TModel> query = RepositoryContext.Set<TModel>().Where(predicate);
        if (include != null)
            query = include(query);
        if (orderBy != null)
            query = orderBy(query);
        if (disableTracking)
            return await query.AsNoTracking().FirstOrDefaultAsync();
        return await query.FirstOrDefaultAsync();
    }

    public async Task Insert(TModel entity)
    {
        await RepositoryContext.Set<TModel>().AddAsync(entity);
    }

    public async Task InsertRange(List<TModel> entity)
    {
        await RepositoryContext.Set<TModel>().AddRangeAsync(entity);
    }

    public async Task Update(TModel entity)
    {
        await Task.Run(() => RepositoryContext.Set<TModel>().Update(entity));
    }

    public async Task Remove(TModel entity)
    {
        await Task.Run(() => RepositoryContext.Set<TModel>().Remove(entity));
    }
}