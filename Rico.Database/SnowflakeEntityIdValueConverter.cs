using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rico.Abstractions.SnowflakeId;
using System.Reflection;

namespace Rico.Database;

internal sealed class SnowflakeEntityIdValueConverter<T>() : ValueConverter<T, string>(
    id => id.Value.Value,
    value => CreateInstance(value)) where T : SnowflakeEntityId
{
    public static T CreateInstance(string value)
    {
        var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        T instance = (T?)Activator.CreateInstance(
            typeof(T),
            bindingFlags,
            null,
            null,
            null)
            ?? throw new ArgumentNullException(nameof(value));

        var prop = typeof(T).GetProperty(nameof(SnowflakeEntityId.Value));

        prop!.SetValue(instance, new SnowflakeValue(value));

        return instance;
    }
}
