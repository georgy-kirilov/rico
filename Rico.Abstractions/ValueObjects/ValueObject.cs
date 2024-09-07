namespace Rico.Abstractions.ValueObjects;

public abstract record ValueObject<T> : BaseValueObject
{
    protected ValueObject(T value, MaxLength maxLength, Unicode unicode, Precision precision)
        : base(maxLength, unicode, precision)
    {
        Value = value;
    }

    public T Value { get; }
}
