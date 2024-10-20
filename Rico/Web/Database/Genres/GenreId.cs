using Rico.ValueObjects;

namespace Web.Database.Genres;

public sealed record GenreId() : ValueObject<short>(Length.None, Unicode.None, Precision.None);
