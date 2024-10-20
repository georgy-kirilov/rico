namespace Rico.Ulids;

public abstract record UlidEntityId
{
    public required Ulid Value { get; init; }

    public override string ToString() => Value.ToString();
}
