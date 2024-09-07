using System.Text.Json.Serialization;

namespace Rico.Abstractions.ValueObjects;

public abstract record BaseValueObject
{
    protected BaseValueObject(MaxLength maxLength, Unicode unicode, Precision precision)
    {
        MaxLength = maxLength.Value;
        Unicode = unicode.Value;
        Precision = precision.PrecisionValue;
        Scale = precision.Scale;
    }

    [JsonIgnore]
    internal int MaxLength { get; }

    [JsonIgnore]
    internal bool Unicode { get; }

    [JsonIgnore]
    internal int Precision { get; }

    [JsonIgnore]
    internal int Scale { get; }
}
