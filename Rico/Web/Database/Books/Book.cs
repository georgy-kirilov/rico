using Rico.Database;
using Web.Database.Genres;

namespace Web.Database.Books;

public sealed class Book : Entity<BookId>
{
    private Book() { }

    public required BookTitle Title { get; init; }

    public required BookPages Pages { get; init; }

    public required BookPublishingDate PublishingDate { get; init; }

    public required BookDescription? Description { get; init; }

    public List<Genre> Genres { get; } = [];

    public static Book Create(
        BookTitle title,
        BookPages pages,
        BookPublishingDate publishingDate,
        BookDescription? description)
    {
        return new()
        {
            Id = new() { Value = Ulid.NewUlid() },
            Title = title,
            Pages = pages,
            PublishingDate = publishingDate,
            Description = description,
        };
    }
}
