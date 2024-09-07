namespace Rico.Abstractions.ValueObjects;

public sealed record MaxLength(int Value)
{
    public static readonly MaxLength None = new(0);
}
