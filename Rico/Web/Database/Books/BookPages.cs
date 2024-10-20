using Rico.Validation;
using Rico.ValueObjects;

namespace Web.Database.Books;

public sealed record BookPages : ValueObject<short>
{
    public const short MinCount = 24;
    public const short MaxCount = 21_450;

    private BookPages() : base(Length.None, Unicode.None, Precision.None) { }

    public static BookPages Create(short value)
    {
        DomainException.ThrowIf.LessThanMinValue<BookPages>(value, MinCount);

        DomainException.ThrowIf.GreaterThanMaxValue<BookPages>(value, MaxCount);

        return new() { Value = value };
    }
}
