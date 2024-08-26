namespace Rico.Abstractions.SnowflakeId;

public sealed record SnowflakeValue
{
    internal SnowflakeValue(string value) => Value = value;

    public string Value { get; }

    public override string ToString() => Value;

    public static SnowflakeValue Create(long value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "Snowflake ID value cannot be less than zero.");
        }

        return new(value.ToString());
    }

    internal static SnowflakeValue Create(string? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        var numericValue = long.Parse(value);

        return Create(numericValue);
    }
}
