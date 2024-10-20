using Rico.Validation;
using Rico.ValueObjects;

namespace Web.Database.Genres;

public sealed record GenreName : ValueObject<string>
{
    public const int MaxLength = 30;

    private GenreName() : base(Length.Max(MaxLength), Unicode.None, Precision.None) { }

    public static GenreName Create(string? value)
    {
        value ??= string.Empty;

        DomainException.ThrowIf.NullEmptyOrWhiteSpace<GenreName>(value);

        value = value.Trim();

        DomainException.ThrowIf.MaxLengthIsExceeded<GenreName>(value, MaxLength);

        DomainException.ThrowIf.UnicodesExist<GenreName>(value);

        return new() { Value = value };
    }
}
