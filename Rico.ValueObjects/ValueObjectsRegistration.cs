using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Rico.ValueObjects;

public static class ValueObjectsRegistration
{
    public static IServiceCollection AddValueObjects(this IServiceCollection services)
    {
        var httpContextAccessor = new HttpContextAccessor();
        
        services.AddSingleton<IHttpContextAccessor>(httpContextAccessor);

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new ValueObjectJsonConverter<string>(httpContextAccessor));
            options.SerializerOptions.Converters.Add(new ValueObjectJsonConverter<byte>(httpContextAccessor));
            options.SerializerOptions.Converters.Add(new ValueObjectJsonConverter<DateTime>(httpContextAccessor));
            options.SerializerOptions.Converters.Add(new ValueObjectJsonConverter<int>(httpContextAccessor));
            options.SerializerOptions.Converters.Add(new ValueObjectJsonConverter<long>(httpContextAccessor));
        });

        services.AddScoped<IModelState, ModelState>();

        return services;
    }
}
