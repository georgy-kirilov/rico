using ErrorOr;
using Microsoft.AspNetCore.Http;
using Rico.Abstractions.ValueObjects;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rico.ValueObjects;

internal sealed class ValueObjectJsonConverter<T>(IHttpContextAccessor _httpContextAccessor) : JsonConverter<ValueObject<T>>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(ValueObject<T>).IsAssignableFrom(typeToConvert);
    }

    public override ValueObject<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = ConvertType(ref reader, typeof(T));

        var createMethod = typeToConvert
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Where(m => m.GetParameters().Length == 1 && m.Name == "Create")
            .Single();

        var result = createMethod.Invoke(null, [value]) ?? throw new Exception();

        var hasErrors = (bool)result.GetType().GetProperty("IsError")!.GetValue(result)!;

        if (hasErrors)
        {
            var errors = result.GetType().GetProperty("Errors")!.GetValue(result) as List<Error>;

            var modelState = _httpContextAccessor.HttpContext?.GetModelState();

            foreach (var error in errors!)
            {
                modelState!.AddError(typeToConvert.Name, error);
            }

            return null;
        }

        return result.GetType().GetProperty("Value")!.GetValue(result) as ValueObject<T>;
    }

    public static object ConvertType(ref Utf8JsonReader reader, Type typeToConvert)
    {
        if (typeof(int) == typeToConvert)
        {
            return reader.GetInt32();
        }

        if (typeof(long) == typeToConvert)
        {
            return reader.GetInt64();
        }

        if (typeof(decimal) == typeToConvert)
        {
            return reader.GetDecimal();
        }

        if (typeof(byte) == typeToConvert)
        {
            return reader.GetByte();
        }

        if (typeof(DateTime) == typeToConvert)
        {
            return reader.GetDateTime();
        }

        return reader.GetString()!;
    }

    public override void Write(Utf8JsonWriter writer, ValueObject<T> value, JsonSerializerOptions options)
    {
        if (IsNumericType(typeof(T)))
        {
            writer.WriteNumberValue(decimal.Parse(value.Value!.ToString()!));
        }
        else if (typeof(T) == typeof(DateTime))
        {
            writer.WriteRawValue(JsonSerializer.Serialize(value.Value));
        }
        else
        {
            writer.WriteStringValue(value.Value!.ToString());
        }
    }

    public static bool IsNumericType(Type type)
    {
        return type == typeof(byte) || type == typeof(sbyte) ||
               type == typeof(short) || type == typeof(ushort) ||
               type == typeof(int) || type == typeof(uint) ||
               type == typeof(long) || type == typeof(ulong) ||
               type == typeof(float) || type == typeof(double) ||
               type == typeof(decimal);
    }
}
