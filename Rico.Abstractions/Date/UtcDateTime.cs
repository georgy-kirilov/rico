namespace Rico.Abstractions.Date;

public sealed record UtcDateTime
{
    internal UtcDateTime(DateTimeOffset value) => Value = value;

    public DateTimeOffset Value { get; }
}
