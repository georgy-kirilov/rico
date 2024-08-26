namespace Rico.Abstractions.ValueObjects;

public abstract record ValueObject<T> : BaseValueObject
{
    protected ValueObject(
        T value,
        int? maxLength = null,
        bool? unicode = null,
        int? precision = null,
        int? scale = null) : base(
            maxLength, 
            unicode,
            precision,
            scale)
    {
        Value = value;
    }

    public T Value { get; }
}
