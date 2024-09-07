using ErrorOr;
using Rico.Abstractions.ValueObjects;

namespace ExampleApp.Domain.Movies;

public sealed record MovieRating : ValueObject<byte>
{
    private MovieRating(byte value) : base(value, MaxLength.None, Unicode.None, Precision.None) { }

    public static ErrorOr<MovieRating> Create(byte value) => new MovieRating(value);
}
