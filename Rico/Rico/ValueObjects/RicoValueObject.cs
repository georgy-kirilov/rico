namespace Rico.ValueObjects;

public abstract record RicoValueObject<T> where T : IComparable<T>
{
    protected RicoValueObject(Length length, Unicode unicode, Precision precision)
    {
        Length = length;
        Unicode = unicode;
        Precision = precision;
    }

    public required T Value { get; init; }

    internal Length Length { get; }

    internal Unicode Unicode { get; }
    
    internal Precision Precision { get; }

    public static bool operator >(RicoValueObject<T> left, RicoValueObject<T> right)
    {
        return left.Value.CompareTo(right.Value) > 0;
    }

    public static bool operator <(RicoValueObject<T> left, RicoValueObject<T> right)
    {
        return left.Value.CompareTo(right.Value) < 0;
    }

    public static bool operator >=(RicoValueObject<T> left, RicoValueObject<T> right)
    {
        return left.Value.CompareTo(right.Value) >= 0;
    }

    public static bool operator <=(RicoValueObject<T> left, RicoValueObject<T> right)
    {
        return left.Value.CompareTo(right.Value) <= 0;
    }
}
