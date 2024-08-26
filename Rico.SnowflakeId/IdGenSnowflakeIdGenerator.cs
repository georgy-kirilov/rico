using IdGen;
using Rico.Abstractions.SnowflakeId;

namespace Rico.SnowflakeId;

internal sealed class IdGenSnowflakeIdGenerator(IdGenerator _generator) : ISnowflakeId
{
    public SnowflakeValue New() => SnowflakeValue.Create(_generator.CreateId());
}
