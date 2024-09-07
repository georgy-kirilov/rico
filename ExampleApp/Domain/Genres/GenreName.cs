using ErrorOr;
using Rico.Abstractions.ValueObjects;

namespace ExampleApp.Domain.Genres;

public sealed record GenreName : ValueObject<string>
{
    public static readonly MaxLength MaxLength = new(70);
    public static readonly Unicode AllowsUnicode = new(false);

    private GenreName(string value) : base(value, MaxLength, AllowsUnicode, Precision.None) { }

    public static ErrorOr<GenreName> Create(string value)
    {
        value = value.Trim();

        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation("GenreName.Required", "Genre name is a required field.");
        }

        if (value.Length > MaxLength.Value)
        {
            return Error.Validation("GenreName.TooLong", "Genre name cannot exceed {0} characters.", new()
            {
                ["0"] = MaxLength.Value,
            });
        }

        if (!AllowsUnicode.Value && !value.All(char.IsAscii))
        {
            return Error.Validation("GenreName.DoesNotAllowUnicodes", "Genre name cannot contain unicode characters.");
        }

        return new GenreName(value);
    }
}
