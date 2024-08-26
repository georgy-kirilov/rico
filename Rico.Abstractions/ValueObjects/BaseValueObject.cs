using System.Text.Json.Serialization;

namespace Rico.Abstractions.ValueObjects;

public abstract record BaseValueObject
{
    protected BaseValueObject(int? maxLength, bool? unicode, int? precision, int? scale)
    {
        MaxLength = maxLength;
        Unicode = unicode;
        Precision = precision;
        Scale = scale;
    }

    [JsonIgnore]
    internal int? MaxLength { get; }

    [JsonIgnore]
    internal bool? Unicode { get; }

    [JsonIgnore]
    internal int? Precision { get; }

    [JsonIgnore]
    internal int? Scale { get; }
}
