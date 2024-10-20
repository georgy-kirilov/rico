using Microsoft.EntityFrameworkCore;
using Rico.Database;
using Rico.Date;
using Web.Database;
using Web.Database.Books;
using Web.Database.Genres;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDateTime();

builder.Services.AddDbContext<AppDbContext>(options =>
    options
    .UseSqlServer("Server=localhost,1434; Database=mozart; User Id=sa; Password=Qwerty1@; TrustServerCertificate=True;",
    x => x.UseNodaTime()));

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

db.Database.EnsureDeleted();
db.Database.EnsureCreated();

await new Seeder<Genre, GenreId>().Seed(db);

//var dateTime = scope.ServiceProvider.GetRequiredService<IDateTime>();

//var book = Book.Create(
//    BookTitle.Create("The Lord of the Rings"),
//    BookPages.Create(423),
//    BookPublishingDate.Create(new(1954, 07, 29), dateTime),
//    BookDescription.Create("""
//        The Lord of the Rings follows a quest to destroy the One Ring,
//        preventing the Dark Lord Sauron from conquering Middle-earth.
//        """));

//var fantasy = new Genre
//{
//    Id = new() { Value = 1 },
//    Name = GenreName.Create("Fantasy"),
//};

//book.Genres.Add(fantasy);

//db.Set<Book>().Add(book);
//db.SaveChanges();

//var fromDate = BookPublishingDate.Create(new(1900, 01, 01), dateTime);

//var sum = db.Set<Book>().Where(x => x.PublishingDate > fromDate).Sum(x => x.Pages);

//Console.WriteLine(sum);

app.Run();
