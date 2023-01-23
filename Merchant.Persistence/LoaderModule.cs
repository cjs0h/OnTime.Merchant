using Merchant.Domain.Interfaces.Repositories;
using Merchant.Persistence.Data;
using Merchant.Persistence.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Merchant.Persistence;

public static class LoaderModule
{
    public static void AddPersistence(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction)
    {
        System.Diagnostics.Debug.WriteLine("Loading Persistence");
        services.AddDbContext<EfContext>(optionsAction);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}