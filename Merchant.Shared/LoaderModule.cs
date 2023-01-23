using Merchant.Application.Interfaces.Shared;
using Merchant.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Merchant.Shared;

public static class LoaderModule
{
    public static void AddShared(this IServiceCollection services)
    {
        System.Diagnostics.Debug.WriteLine("Loading Shared");
        services.AddScoped<IUserService, UserService>();
    }
}