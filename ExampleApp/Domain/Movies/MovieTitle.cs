using ErrorOr;
using Rico.Abstractions.ValueObjects;

namespace ExampleApp.Domain.Movies;

public sealed record MovieTitle : ValueObject<string>
{
    public static readonly MaxLength MaxLength = new(100);
    public static readonly Unicode Unicode = new(true);

    private MovieTitle(string value) : base(value, MaxLength, Unicode, Precision.None) { }

    public static ErrorOr<MovieTitle> Create(string value)
    {
        value = value.Trim();

        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation("TitleIsRequired", "Title is required.");
        }

        if (value.Length > MaxLength.Value)
        {
            return Error.Validation("TitleTooLong", "Title cannot exceed {0} characters.", new()
            {
                ["0"] = MaxLength.Value
            });
        }

        return new MovieTitle(value);
    }
}
