using Rico.Validation;
using Rico.ValueObjects;

namespace Web.Database.Books;

public sealed record BookTitle : ValueObject<string>
{
    public const int MaxLength = 70;

    private BookTitle() : base(Length.Max(MaxLength), Unicode.Allowed, Precision.None) { }

    public static BookTitle Create(string? value)
    {
        value ??= string.Empty;

        DomainException.ThrowIf.NullEmptyOrWhiteSpace<BookTitle>(value);

        value = value.Trim();

        DomainException.ThrowIf.MaxLengthIsExceeded<BookTitle>(value, MaxLength);

        return new() { Value = value };
    }
}
