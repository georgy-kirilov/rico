using IdGen.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Rico.Abstractions.SnowflakeId;

namespace Rico.SnowflakeId;

public static class ServiceRegistration
{
    public static IServiceCollection AddSnowflakeId(this IServiceCollection services, int generatorId)
    {
        services.AddIdGen(generatorId);

        services.AddSingleton<ISnowflakeId, IdGenSnowflakeIdGenerator>();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new SnowflakeEntityIdJsonConverter());
        });

        return services;
    }
}
