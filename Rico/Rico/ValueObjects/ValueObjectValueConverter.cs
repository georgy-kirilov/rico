using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Rico.ValueObjects;

public sealed class ValueObjectValueConverter<TModel, TValue>() : ValueConverter<TModel, TValue>(
    x => x.Value,
    x => CreateInstance(x))
    where TModel : ValueObject<TValue>
    where TValue : IComparable<TValue>
{
    public static TModel CreateInstance(TValue value)
    {
        TModel instance = Activator.CreateInstance(typeof(TModel), nonPublic: true) as TModel
            ?? throw new ArgumentNullException(nameof(value));

        typeof(TModel).GetProperty(nameof(ValueObject<TValue>.Value))!.SetValue(instance, value);

        return instance;
    }
}
