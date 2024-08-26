using ErrorOr;
using Rico.Abstractions.ValueObjects;

namespace ExampleApp;

public sealed record MovieTitle : StringValueObject
{
    public const int MaxLength = 100;

    private MovieTitle(string value) : base(value, MaxLength, true) { }

    public static ErrorOr<MovieTitle> Create(string value)
    {
        value = value.Trim();

        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.TitleIsRequired;
        }

        if (value.Length > MaxLength)
        {
            return Errors.TitleTooLong;
        }

        return new MovieTitle(value);
    }

    public static class Errors
    {
        public static readonly Error TitleIsRequired = Error.Validation(nameof(TitleIsRequired), "Title is required.");

        public static readonly Error TitleTooLong = Error.Validation(nameof(TitleTooLong), $"Title cannot exceed {MaxLength} characters.", new()
        {
            [nameof(MaxLength)] = MaxLength
        });
    }
}
