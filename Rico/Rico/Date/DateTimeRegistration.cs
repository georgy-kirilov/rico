using Microsoft.Extensions.DependencyInjection;

namespace Rico.Date;

public static class DateTimeRegistration
{
    public static IServiceCollection AddDateTime(this IServiceCollection services)
    {
        services.AddSingleton<IDateTime, NodaTimeDateTimeProvider>();
        return services;
    }
}
