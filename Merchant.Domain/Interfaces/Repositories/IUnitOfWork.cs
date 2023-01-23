using Merchant.Domain.Base;

namespace Merchant.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IRepositoryBase<T> Repository<T, TId>() where T : BaseEntity<TId> where TId : struct;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    int SaveChanges();

    int SaveChanges(bool acceptAllChangesOnSuccess);
    Task Rollback();
}