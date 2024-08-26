using Rico.SnowflakeId;
using Rico.Date;
using Rico.Database;
using Microsoft.EntityFrameworkCore;
using ExampleApp;
using Rico.Abstractions.SnowflakeId;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase<MoviesDbContext>(options => options.UseSqlServer("Server=localhost,1434; Database=mozart; User Id=sa; Password=Qwerty1@; TrustServerCertificate=True;"));
builder.Services.AddDate();
builder.Services.AddSnowflakeId(1);

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var db = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();
var snowflakeId = scope.ServiceProvider.GetRequiredService<ISnowflakeId>();

db.Database.EnsureDeleted();
db.Database.EnsureCreated();

var movieId = new MovieId { Value = snowflakeId.New() };
var movieTitle = MovieTitle.Create("Titanic").Value;
var releaseDate = MovieReleaseDate.Create(new DateTime(1997, 12, 19));

db.Set<Movie>().Add(Movie.Create(movieId, movieTitle, releaseDate));
db.SaveChanges();

app.MapGet("/movies", (MoviesDbContext db) => db.Set<Movie>().ToList());

app.Run();
