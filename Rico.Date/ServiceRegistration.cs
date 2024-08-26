using Microsoft.Extensions.DependencyInjection;
using Rico.Abstractions.Date;

namespace Rico.Date;

public static class ServiceRegistration
{
    public static IServiceCollection AddDate(this IServiceCollection services)
    {
        services.AddSingleton<IUtcDateTime, SystemUtcDateTimeProvider>();
        return services;
    }
}
