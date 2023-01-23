using System;
using Merchant.Application;
using Merchant.Common;
using Merchant.Domain;
using Merchant.Persistence;
using Merchant.Persistence.Data;
using Merchant.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Merchant.Tests;

public class TestBase : IDisposable
{
    public readonly EfContext Context;
    public readonly IServiceProvider ServiceProvider;
    public TestBase()
    {
        var configuration = BuildConfiguration();
        var serviceProvider = BuildServiceProvider(configuration);
        ServiceProvider = serviceProvider;
        Context = serviceProvider.GetService<EfContext>();
    }
    private IServiceProvider BuildServiceProvider(IConfiguration configuration)
    {
        IServiceCollection services = new ServiceCollection();
        services.AddApplication();
        services.AddCommon();
        services.AddDomain();
        services.AddPersistence(options => options.UseInMemoryDatabase("IdentityDb"));
        services.AddShared();
        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        services.AddLogging();

        return services.BuildServiceProvider();
    }
    private IConfiguration BuildConfiguration()
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        configuration["UseInMemoryDatabase"] = "true";
        return configuration;
    }

    public void AddUserData(EfContext context)
    {
        context.Database.EnsureCreated();
        DbInitializer.Initialize(context);
    }
    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}