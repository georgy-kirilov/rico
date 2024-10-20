using Rico.Validation;
using Rico.ValueObjects;

namespace Web.Database.Books;

public sealed record BookDescription : ValueObject<string>
{
    public const int MaxLength = 500;

    private BookDescription() : base(Length.Max(MaxLength), Unicode.Allowed, Precision.None) { }

    public static BookDescription? Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        value = value.Trim();

        DomainException.ThrowIf.MaxLengthIsExceeded<BookDescription>(value, MaxLength);

        return new() { Value = value };
    }
}
