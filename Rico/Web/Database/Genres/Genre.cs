using Rico.Database;
using Web.Database.Books;

namespace Web.Database.Genres;

public sealed class Genre : Entity<GenreId>
{
    public required GenreName Name { get; init; }

    public List<Book> Books { get; } = [];
}
