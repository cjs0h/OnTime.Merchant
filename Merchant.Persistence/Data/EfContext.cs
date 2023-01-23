using Merchant.Domain.Base;
using Merchant.Domain.Entities;
using Merchant.Domain.Entities.OrderAttributes;
using Merchant.Domain.Entities.ProductAttributes;
using Merchant.Domain.Entities.ReferenceNumber;
using Merchant.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Merchant.Persistence.Data;

public class EfContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Payment> Payment { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<ReferenceNumberType> ReferenceNumberTypes { get; set; }
    public virtual DbSet<ReferenceNumberValue> ReferenceNumberValues { get; set; }
    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<Unit> Units { get; set; }
    public virtual DbSet<Discount> Discounts { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{

    //    optionsBuilder.UseNpgsql(
    //        "Server=localhost;Port=5432;Database=MerchantDb;User Id=postgres;Password=aaa-1994;");
    //}

    public EfContext(DbContextOptions<EfContext> options) : base(options)
    {
    }
    public EfContext()
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        foreach (var type in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IEntity).IsAssignableFrom(type.ClrType))
            {
                modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
        }
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    private void OnBeforeSaving()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IEntity && e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        foreach (var entityEntry in entries)
        {
            ((IEntity)entityEntry.Entity).UpdateDate = DateTime.Now.ToUniversalTime();
            ((IEntity)entityEntry.Entity).IsDeleted = false;

            switch (entityEntry.State)
            {
                case EntityState.Added:
                    ((IEntity)entityEntry.Entity).CreateDate = DateTime.Now.ToUniversalTime();
                    break;
                case EntityState.Deleted:
                    entityEntry.State = EntityState.Modified;
                    ((IEntity)entityEntry.Entity).IsDeleted = true;
                    break;
            }
        }
    }
}