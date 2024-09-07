using Rico.SnowflakeId;
using Rico.Date;
using Rico.Database;
using Microsoft.EntityFrameworkCore;
using ExampleApp;
using Rico.Abstractions.SnowflakeId;
using Rico.ValueObjects;
using ExampleApp.Domain.Movies;
using ExampleApp.Domain.Genres;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase<MoviesDbContext>(options => options.UseSqlServer("Server=localhost,1434; Database=mozart; User Id=sa; Password=Qwerty1@; TrustServerCertificate=True;"));
builder.Services.AddDate();
builder.Services.AddSnowflakeId(1);
builder.Services.AddValueObjects();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var db = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();
var snowflakeId = scope.ServiceProvider.GetRequiredService<ISnowflakeId>();

db.Database.EnsureDeleted();
db.Database.EnsureCreated();

var movieId = new MovieId { Value = snowflakeId.New() };
var movieTitle = MovieTitle.Create("Titanic").Value;
var releaseDate = MovieReleaseDate.Create(new DateTime(2024, 12, 19)).Value;
var movie = Movie.Create(movieId, movieTitle, releaseDate, MovieRating.Create(76).Value);

movie.Genres.Add(Genre.Create(new GenreId { Value = snowflakeId.New() }, GenreName.Create("Romantic").Value));

db.Set<Movie>().Add(movie);
db.SaveChanges();

app.UseMiddleware<ModelStateMiddleware>();

app.MapGet("/movies", (MoviesDbContext db) => db.Set<Movie>().ToList());

app.MapPost("/movies", (MovieDto movie, IModelState modelState) =>
{
    if (modelState.HasErrors(movie))
    {
        return modelState.ToApiResult(movie);
    }

    return Results.Ok(movie);
});

app.Run();
