using NodaTime;

namespace Rico.Date;

internal sealed class NodaTimeDateTimeProvider : IDateTime
{
    public LocalDate GetToday()
    {
        var timeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();

        Instant now = SystemClock.Instance.GetCurrentInstant();

        return now.InZone(timeZone).Date;
    }
}
