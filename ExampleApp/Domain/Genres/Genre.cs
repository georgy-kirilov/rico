using ExampleApp.Domain.Movies;
using Rico.Abstractions.Entities;
using Rico.Abstractions.SnowflakeId;

namespace ExampleApp.Domain.Genres;

public sealed record GenreId : SnowflakeEntityId;

public sealed class Genre : Entity<GenreId>
{
    public required GenreName Name { get; init; }

    public List<Movie> Movies { get; } = [];

    private Genre() { }

    public static Genre Create(GenreId id, GenreName name) => new()
    {
        Id = id,
        Name = name,
    };
}
