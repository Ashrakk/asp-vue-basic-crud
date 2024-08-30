using CorsoASP.Core.Interfaces.IRepository;
using CorsoASP.Core.Interfaces.IService;
using CorsoASP.Infrastructure.Repository;
using CorsoASP.Infrastructure.Services;

namespace CorsoASP.UI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterService(this IServiceCollection services)
    {
        services.AddTransient<IWalksRepository, WalksRepository>();
        services.AddTransient<IRegionsRepository, RegionsRepository>();
        services.AddTransient<IDifficultyRepository, DifficultyRepository>();
        
        services.AddScoped<IWalksService, WalksService>();
        services.AddScoped<IRegionsService, RegionsService>();
        services.AddScoped<IDifficultyService, DifficultyService>();
        
        return services;
    }
}