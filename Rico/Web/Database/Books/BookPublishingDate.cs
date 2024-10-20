using System.Text.Json.Serialization;
using NodaTime;
using Rico.Date;
using Rico.Validation;
using Rico.ValueObjects;

namespace Web.Database.Books;

public sealed record BookPublishingDate : ValueObject<LocalDate>
{
    public static readonly LocalDate GutenbergPrintingPressInventionDate = new(1450, 1, 1);

    [JsonConstructor]
    private BookPublishingDate() : base(Length.None, Unicode.None, Precision.None) { }

    public static BookPublishingDate Create(LocalDate value, IDateTime dateTime)
    {
        DomainException.ThrowIf.BeforeMinDate<BookPublishingDate>(value, GutenbergPrintingPressInventionDate);

        DomainException.ThrowIf.AfterMaxDate<BookPublishingDate>(value, dateTime.GetToday());

        return new() { Value = value };
    }
}
