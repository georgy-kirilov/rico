using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rico.Abstractions.ValueObjects;
using System.Reflection;

namespace Rico.Database;

public sealed class ValueObjectValueConverter<TModel, TValue>() : ValueConverter<TModel, TValue>(
    valueObject => valueObject.Value,
    valueObject => CreateInstance(valueObject)) where TModel : ValueObject<TValue>
{
    public static TModel CreateInstance(TValue value)
    {
        var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        TModel instance = (TModel?)Activator.CreateInstance(
            typeof(TModel),
            bindingFlags,
            null,
            [value],
            null)
            ?? throw new ArgumentNullException(nameof(value));

        return instance;
    }
}
