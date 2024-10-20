using System.Globalization;
using NodaTime;
using Rico.ValueObjects;

namespace Rico.Validation;

public sealed class DomainException : Exception
{
    private DomainException(string message, DomainExceptionType type) : base(message)
    {
        Type = type;
    }

    public DomainExceptionType Type { get; }

    public static class ThrowIf
    {
        public static void MaxLengthIsExceeded<T>(string value, int maxLength) where T : ValueObject<string>
        {
            if (value.Length > maxLength)
            {
                var paramName = nameof(T);
                var message = $"{paramName} cannot exceed {maxLength} characters.";
                throw new DomainException(message, DomainExceptionType.MaxLengthIsExceeded);
            }
        }

        public static void UnicodesExist<T>(string value) where T : ValueObject<string>
        {
            if (value.Any(c => !char.IsAscii(c)))
            {
                var paramName = nameof(T);
                var message = $"{paramName} cannot contain unicode characters.";
                throw new DomainException(message, DomainExceptionType.UnicodesExist);
            }
        }

        public static void LessThanZero<T>(short value) where T : ValueObject<short>
        {
            if (value < 0)
            {
                var paramName = nameof(T);
                var message = $"{paramName} cannot be less than zero.";
                throw new DomainException(message, DomainExceptionType.LessThanZero);
            }
        }

        public static void NullEmptyOrWhiteSpace<T>(string? value) where T : ValueObject<string>
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var paramName = nameof(T);
                var message = $"{paramName} is required.";
                throw new DomainException(message, DomainExceptionType.NullEmptyOrWhiteSpace);
            }
        }

        public static void GreaterThanMaxValue<T>(short actual, short maxValue) where T : ValueObject<short>
        {
            if (actual > maxValue)
            {
                var paramName = nameof(T);
                var message = $"{paramName} cannot be greater than {maxValue}.";
                throw new DomainException(message, DomainExceptionType.GreaterThanMaxValue);
            }
        }

        public static void LessThanMinValue<T>(short actual, short minValue) where T : ValueObject<short>
        {
            if (actual < minValue)
            {
                var paramName = nameof(T);
                var message = $"{paramName} cannot be less than {minValue}.";
                throw new DomainException(message, DomainExceptionType.LessThanMinValue);
            }
        }

        public static void BeforeMinDate<T>(LocalDate actual, LocalDate minDate) where T : ValueObject<LocalDate>
        {
            if (actual < minDate)
            {
                var paramName = nameof(T);
                var minDateAsText = minDate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                var message = $"{paramName} cannot be before {minDateAsText}.";
                throw new DomainException(message, DomainExceptionType.BeforeMinDate);
            }
        }

        public static void AfterMaxDate<T>(LocalDate actual, LocalDate maxDate) where T : ValueObject<LocalDate>
        {
            if (actual > maxDate)
            {
                var paramName = nameof(T);
                var minDateAsText = maxDate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
                var message = $"{paramName} cannot be after {minDateAsText}.";
                throw new DomainException(message, DomainExceptionType.AfterMaxDate);
            }
        }
    }
}
