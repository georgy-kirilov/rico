namespace Rico.Database;

public abstract class Entity<TKey>
{
    public required TKey Id { get; init; }
}
