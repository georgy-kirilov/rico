namespace Rico.Abstractions.Date;

internal sealed class SystemUtcDateTimeProvider : IUtcDateTime
{
    public UtcDateTime UtcNow() => new(TimeProvider.System.GetUtcNow());
}
