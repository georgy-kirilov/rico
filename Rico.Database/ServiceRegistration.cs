using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Rico.Database;

public static class ServiceRegistration
{
    public static IServiceCollection AddDatabase<TContext>(this IServiceCollection services,
        Action<DbContextOptionsBuilder> configureDbContextOptions)
        where TContext : BaseDbContext
    {
        services.AddDbContext<TContext>(configureDbContextOptions);

        return services;
    }
}
