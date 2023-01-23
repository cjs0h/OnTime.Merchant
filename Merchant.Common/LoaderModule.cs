using Microsoft.Extensions.DependencyInjection;

namespace Merchant.Common;

public static class LoaderModule
{
    public static void AddCommon(this IServiceCollection services)
    {
        System.Diagnostics.Debug.WriteLine("Loading Common");
    }
}