namespace Rico.Abstractions.ValueObjects;

public abstract record StringValueObject : ValueObject<string>
{
    protected StringValueObject(string value, int maxLength, bool unicode) : base(value, maxLength, unicode, null, null) { }
}
