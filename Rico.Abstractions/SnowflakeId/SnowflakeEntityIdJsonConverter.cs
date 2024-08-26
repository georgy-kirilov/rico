using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rico.Abstractions.SnowflakeId;

public sealed class SnowflakeEntityIdJsonConverter : JsonConverter<SnowflakeEntityId>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(SnowflakeEntityId).IsAssignableFrom(typeToConvert);
    }

    public override SnowflakeEntityId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            throw new JsonException("Snowflake ID value cannot be null.");
        }

        var input = reader.TokenType == JsonTokenType.Number ? reader.GetInt64().ToString() : reader.GetString();

        var snowflakeIdValue = SnowflakeValue.Create(input);

        var snowflakeId = Activator.CreateInstance(typeToConvert) as SnowflakeEntityId;

        ArgumentNullException.ThrowIfNull(snowflakeId, nameof(snowflakeId));

        var property = typeToConvert.GetProperty(nameof(SnowflakeEntityId.Value));

        ArgumentNullException.ThrowIfNull(property, nameof(property));

        property.SetValue(snowflakeId, snowflakeIdValue);

        return snowflakeId;
    }

    public override void Write(Utf8JsonWriter writer, SnowflakeEntityId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value.ToString());
    }
}
