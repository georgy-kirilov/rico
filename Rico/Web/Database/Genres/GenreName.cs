using System.Text.Json.Serialization;
using Rico.Validation;
using Rico.ValueObjects;

namespace Web.Database.Genres;

public sealed record GenreName : ValueObject<string>
{
    public static readonly Length MaxLength = Length.Max(30);

    [JsonConstructor]
    private GenreName() : base(MaxLength, Unicode.None, Precision.None) { }

    public static GenreName Create(string? value)
    {
        value ??= string.Empty;

        DomainException.ThrowIf.NullEmptyOrWhiteSpace<GenreName>(value);

        value = value.Trim();

        DomainException.ThrowIf.MaxLengthIsExceeded<GenreName>(value, MaxLength.Value);

        DomainException.ThrowIf.UnicodesExist<GenreName>(value);

        return new() { Value = value };
    }
}
