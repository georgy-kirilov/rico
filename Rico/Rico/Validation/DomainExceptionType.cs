namespace Rico.Validation;

public enum DomainExceptionType
{
    MaxLengthIsExceeded = 1,
    UnicodesExist = 2,
    LessThanZero = 3,
    NullEmptyOrWhiteSpace = 4,
    GreaterThanMaxValue = 5,
    LessThanMinValue = 6,
    BeforeMinDate = 7,
    AfterMaxDate = 8,
}
