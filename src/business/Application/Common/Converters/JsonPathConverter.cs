using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application.Common.Converters;

/// <summary>
/// Custom JSON converter that uses JsonPath expressions to deserialize JSON into objects.
/// Supports mapping JSON properties to object properties using JsonPropertyAttribute paths.
/// </summary>
public class JsonPathConverter : JsonConverter
{
    /// <summary>
    /// Reads JSON and converts it to an object using JsonPath expressions.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="objectType">The type of object to create.</param>
    /// <param name="existingValue">The existing value of the object being read.</param>
    /// <param name="serializer">The JSON serializer.</param>
    /// <returns>The deserialized object.</returns>
    public override object? ReadJson(JsonReader reader, Type objectType,
        object? existingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);
        object? targetObj = Activator.CreateInstance(objectType);

        foreach (PropertyInfo prop in objectType.GetProperties()
                     .Where(p => p is { CanRead: true, CanWrite: true }))
        {
            JsonPropertyAttribute? att = prop.GetCustomAttributes(true)
                .OfType<JsonPropertyAttribute>()
                .FirstOrDefault();

            string? jsonPath = (att != null ? att.PropertyName : prop.Name);

            if (jsonPath == null)
            {
                continue;
            }

            JToken? token = jo.SelectToken(jsonPath);

            if (token == null || token.Type == JTokenType.Null)
            {
                continue;
            }

            object? value = token.ToObject(prop.PropertyType, serializer);
            prop.SetValue(targetObj, value, null);
        }

        return targetObj;
    }

    /// <summary>
    /// Determines whether this converter can convert the specified object type.
    /// </summary>
    /// <param name="objectType">The type of object to check.</param>
    /// <returns>Always returns false as conversion is explicit.</returns>
    public override bool CanConvert(Type objectType)
    {
        return false;
    }

    /// <summary>
    /// Gets a value indicating whether this converter can write JSON.
    /// </summary>
    public override bool CanWrite => false;

    /// <summary>
    /// Writes JSON representation of the object.
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