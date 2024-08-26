namespace Rico.Abstractions.Entities;

public abstract record EntityId<T> : IEntityId
{
    public required T Value { get; init; }
}
