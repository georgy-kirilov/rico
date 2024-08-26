namespace Rico.Abstractions.ValueObjects;

public abstract record DateTimeValueObject : ValueObject<DateTime>
{
    protected DateTimeValueObject(DateTime value, int precision) : base(value, null, null, precision, null) { }
}
