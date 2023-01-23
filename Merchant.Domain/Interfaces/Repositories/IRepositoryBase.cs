using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Merchant.Domain.Interfaces.Repositories;

public interface IRepositoryBase<T> : IDisposable
{
    IQueryable<T> FindAll();

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate = null!,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null!,
        bool disableTracking = true);
    Task<T?> FindItemByCondition(Expression<Func<T, bool>> predicate = null!,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null!,
        bool disableTracking = true);
    Task Insert(T entity);
    Task InsertRange(List<T> entity);
    Task Update(T entity);
    Task Remove(T entity);
}