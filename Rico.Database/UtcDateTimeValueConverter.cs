using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rico.Abstractions.Date;

namespace Rico.Database;

internal sealed class UtcDateTimeValueConverter() : ValueConverter<UtcDateTime, DateTimeOffset>
(
    id => id.Value,
    value => new(value)
);
