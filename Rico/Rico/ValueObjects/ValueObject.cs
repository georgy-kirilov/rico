namespace Rico.ValueObjects;

public abstract record ValueObject<T> where T : IComparable<T>
{
    protected ValueObject(Length length, Unicode unicode, Precision precision)
    {
        Length = length;
        Unicode = unicode;
        Precision = precision;
    }

    public required T Value { get; init; }

    internal Length Length { get; }

    internal Unicode Unicode { get; }
    
    internal Precision Precision { get; }

    public static bool operator >(ValueObject<T> left, ValueObject<T> right)
    {
        return left.Value.CompareTo(right.Value) > 0;
    }

    public static bool operator <(ValueObject<T> left, ValueObject<T> right)
    {
        return left.Value.CompareTo(right.Value) < 0;
    }

    public static bool operator >=(ValueObject<T> left, ValueObject<T> right)
    {
        return left.Value.CompareTo(right.Value) >= 0;
    }

    public static bool operator <=(ValueObject<T> left, ValueObject<T> right)
    {
        return left.Value.CompareTo(right.Value) <= 0;
    }
}
