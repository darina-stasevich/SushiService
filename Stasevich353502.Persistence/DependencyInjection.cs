using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stasevich353502.Persistence.Data;
using Stasevich353502.Persistence.Repository;

namespace Stasevich353502.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
        return services;
    }
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        DbContextOptions options)
    {
        services.AddPersistence()
            .AddSingleton<AppDbContext>(
                new AppDbContext((DbContextOptions<AppDbContext>)options));
        return services; 
    }
}