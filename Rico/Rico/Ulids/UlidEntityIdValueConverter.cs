using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Rico.Ulids;

internal sealed class UlidEntityIdValueConverter<T>() : ValueConverter<T, string>(
    id => id.Value.ToString(),
    value => CreateInstance(value)) where T : UlidEntityId
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

        var prop = typeof(T).GetProperty(nameof(UlidEntityId.Value));

        prop!.SetValue(instance, Ulid.Parse(value));

        return instance;
    }
}
