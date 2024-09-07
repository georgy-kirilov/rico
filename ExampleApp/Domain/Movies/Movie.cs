using ExampleApp.Domain.Genres;
using Rico.Abstractions.Entities;
using Rico.Abstractions.SnowflakeId;

namespace ExampleApp.Domain.Movies;

public sealed record MovieId : SnowflakeEntityId;

public sealed class Movie : Entity<MovieId>
{
    public required MovieTitle Title { get; init; }

    public required MovieReleaseDate ReleaseDate { get; init; }

    public required MovieRating Rating { get; init; }

    public List<Genre> Genres { get; } = [];

    private Movie() { }

    public static Movie Create(
        MovieId id,
        MovieTitle title,
        MovieReleaseDate releaseDate,
        MovieRating movieRating) => new()
        {
            Id = id,
            Title = title,
            ReleaseDate = releaseDate,
            Rating = movieRating,
        };
}
