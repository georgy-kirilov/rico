using System.Text.Json.Serialization;
using Rico.Validation;
using Rico.ValueObjects;

namespace Web.Database.Books;

public sealed record BookDescription : ValueObject<string>
{
    public static readonly Length MaxLength = Length.Max(500);

    [JsonConstructor]
    private BookDescription() : base(MaxLength, Unicode.Allowed, Precision.None) { }

    public static BookDescription? Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        value = value.Trim();

        DomainException.ThrowIf.MaxLengthIsExceeded<BookDescription>(value, MaxLength.Value);

        return new() { Value = value };
    }
}
