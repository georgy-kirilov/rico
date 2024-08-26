using Rico.Abstractions.Entities;

namespace Rico.Abstractions.SnowflakeId;

public abstract record SnowflakeEntityId : EntityId<SnowflakeValue>
{
    public const int MaxLength = 19;

    public override string ToString() => Value.ToString();
}
