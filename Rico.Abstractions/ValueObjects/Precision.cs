namespace Rico.Abstractions.ValueObjects;

public sealed record Precision(int PrecisionValue, int Scale = 0)
{
    public static readonly Precision None = new(0, 0);
}
