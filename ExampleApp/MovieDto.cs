using ExampleApp.Domain.Movies;

namespace ExampleApp;

public sealed record MovieDto(MovieTitle Title, MovieReleaseDate ReleaseDate, MovieRating Rating);
