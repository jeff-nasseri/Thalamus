using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application.Common.Converters;

/// <summary>
///     Custom JSON converter that uses JsonPath expressions to deserialize JSON into objects.
///     Supports mapping JSON properties to object properties using JsonPropertyAttribute paths.
/// </summary>
public class JsonPathConverter : JsonConverter
{
    /// <summary>
    ///     Gets a value indicating whether this converter can write JSON.
    /// </summary>
    public override bool CanWrite => false;

    /// <summary>
    ///     Reads JSON and converts it to an object using JsonPath expressions.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="objectType">The type of object to create.</param>
    /// <param name="existingValue">The existing value of the object being read.</param>
    /// <param name="serializer">The JSON serializer.</param>
    /// <returns>The deserialized object.</returns>
    public override object? ReadJson(JsonReader reader, Type objectType,
        object? existingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        var targetObj = Activator.CreateInstance(objectType);

        foreach (var prop in objectType.GetProperties()
                     .Where(p => p is { CanRead: true, CanWrite: true }))
        {
            var att = prop.GetCustomAttributes(true)
                .OfType<JsonPropertyAttribute>()
                .FirstOrDefault();

            var jsonPath = att != null ? att.PropertyName : prop.Name;

            if (jsonPath == null) continue;

            var token = jo.SelectToken(jsonPath);

            if (token == null || token.Type == JTokenType.Null) continue;

            var value = token.ToObject(prop.PropertyType, serializer);
            prop.SetValue(targetObj, value, null);
        }

        return targetObj;
    }

    /// <summary>
    ///     Determines whether this converter can convert the specified object type.
    /// </summary>
    /// <param name="objectType">The type of object to check.</param>
    /// <returns>Always returns false as conversion is explicit.</returns>
    public override bool CanConvert(Type objectType)
    {
        return false;
    }

    /// <summary>
    ///     Writes JSON representation of the object.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="serializer">The JSON serializer.</param>
    /// <exception cref="NotImplementedException">This converter does not support writing JSON.</exception>
    public override void WriteJson(JsonWriter writer, object? value,
        JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}