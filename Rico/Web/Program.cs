using Microsoft.EntityFrameworkCore;
using Rico.Date;
using Web.Database;
using Web.Database.Books;

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

var dateTime = scope.ServiceProvider.GetRequiredService<IDateTime>();

var book = Book.Create(
    BookTitle.Create("The Lord of the Rings"),
    BookPages.Create(423),
    BookPublishingDate.Create(new(1954, 07, 29), dateTime),
    BookDescription.Create("""
        The Lord of the Rings follows a quest to destroy the One Ring,
        preventing the Dark Lord Sauron from conquering Middle-earth.
        """));

db.Set<Book>().Add(book);
db.SaveChanges();

app.Run();
