using Microsoft.Extensions.DependencyInjection;

namespace Merchant.Domain;

public static class LoaderModule
{
    public static void AddDomain(this IServiceCollection services)
    {
        System.Diagnostics.Debug.WriteLine("Loading Domain");
    }
}