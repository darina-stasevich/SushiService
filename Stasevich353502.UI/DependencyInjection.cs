using Stasevich353502.UI.Pages;
using Stasevich353502.UI.ViewModels;

namespace Stasevich353502.UI;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPages(this IServiceCollection services)
    {
        services.AddTransient<SushiSets>();
        return services;
    }
    
    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.AddTransient<SushiSetsViewModel>();
        return services;
    }
}