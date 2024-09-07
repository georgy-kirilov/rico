using ErrorOr;
using Rico.Abstractions.ValueObjects;

namespace ExampleApp.Domain.Movies;

public sealed record MovieReleaseDate : ValueObject<DateTime>
{
    private MovieReleaseDate(DateTime value) : base(value, MaxLength.None, Unicode.None, new Precision(3)) { }

    public static ErrorOr<MovieReleaseDate> Create(DateTime value)
    {
        if (value.Year != 2024)
        {
            return Error.Validation("YearMustBe2024", "MovieReleaseDate Year must be 2024.");
        }

        return new MovieReleaseDate(value);
    }
}
