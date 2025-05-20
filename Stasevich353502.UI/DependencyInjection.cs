using CommunityToolkit.Maui;
using Stasevich353502.UI.Pages;
using Stasevich353502.UI.ViewModels;

namespace Stasevich353502.UI;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPages(this IServiceCollection services)
    {
        services.AddSingleton<SushiSets>();
        services.AddTransient<SushiDetailsPage>();
        services.AddTransient<CreateSushiSetPage>();
        services.AddTransient<CreateSushiInSushiSetPage>();
        return services;
    }
    
    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.AddSingleton<SushiSetsViewModel>();
        services.AddTransient<SushiDetailsViewModel>();
        services.AddTransient<CreateSushiSetViewModel>();
        services.AddTransient<CreateSushiInSushiSetViewModel>();
        return services;
    }
}