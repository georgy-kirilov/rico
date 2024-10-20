using System.Text.Json.Serialization;
using Rico.Validation;
using Rico.ValueObjects;

namespace Web.Database.Books;

public sealed record BookTitle : ValueObject<string>
{
    public static readonly Length MaxLength = Length.Max(70);

    [JsonConstructor]
    private BookTitle() : base(MaxLength, Unicode.Allowed, Precision.None) { }

    public static BookTitle Create(string? value)
    {
        value ??= string.Empty;

        DomainException.ThrowIf.NullEmptyOrWhiteSpace<BookTitle>(value);

        value = value.Trim();

        DomainException.ThrowIf.MaxLengthIsExceeded<BookTitle>(value, MaxLength.Value);

        return new() { Value = value };
    }
}
