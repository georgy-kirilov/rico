namespace Rico.Abstractions.ValueObjects;

public sealed record Unicode(bool Value)
{
    public static readonly Unicode None = new(false);
}
