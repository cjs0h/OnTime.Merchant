using Merchant.Application.Interfaces.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Merchant.Application;

public static class LoaderModule
{
    public static void AddApplication(this IServiceCollection services)
    {
        System.Diagnostics.Debug.WriteLine("Loading Application");
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}