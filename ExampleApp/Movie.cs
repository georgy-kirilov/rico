using Rico.Abstractions.Entities;
using Rico.Abstractions.SnowflakeId;

namespace ExampleApp;

public sealed record MovieId : SnowflakeEntityId;

public sealed class Movie : Entity<MovieId>
{
    public required MovieTitle Title { get; init; }

    public required MovieReleaseDate ReleaseDate { get; init; }

    private Movie() { }

    public static Movie Create(MovieId id, MovieTitle title, MovieReleaseDate releaseDate) => new()
    {
        Id = id,
        Title = title,
        ReleaseDate = releaseDate,
    };
}
