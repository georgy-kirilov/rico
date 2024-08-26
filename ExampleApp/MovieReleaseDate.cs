using Rico.Abstractions.ValueObjects;

namespace ExampleApp;

public sealed record MovieReleaseDate : DateTimeValueObject
{
    private MovieReleaseDate(DateTime value) : base(value, 3) { }

    public static MovieReleaseDate Create(DateTime value)
    {
        return new MovieReleaseDate(value);
    }
}
