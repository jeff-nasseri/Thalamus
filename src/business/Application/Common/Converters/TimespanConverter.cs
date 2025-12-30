using Application.Common.Extensions;
using Newtonsoft.Json;

namespace Application.Common.Converters;

/// <summary>
/// Custom JSON converter for TimeSpan values that converts them to/from readable string format.
/// </summary>
public class TimespanConverter : JsonConverter<TimeSpan>
{
    /// <summary>
    /// Writes a TimeSpan value to JSON in a human-readable string format.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The TimeSpan value to write.</param>
    /// <param name="serializer">The JSON serializer.</param>
    public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
    {
        string format = value.ToReadableString();
        writer.WriteValue(format);
    }

    /// <summary>
    /// Reads a TimeSpan value from JSON string format.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="objectType">The type of object being read.</param>
    /// <param name="existingValue">The existing TimeSpan value.</param>
    /// <param name="hasExistingValue">Indicates whether an existing value is present.</param>
    /// <param name="serializer">The JSON serializer.</param>
    /// <returns>The parsed TimeSpan value.</returns>
    public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        TimeSpan.TryParseExact((string)reader.Value!, existingValue.ToReadableString(), null, out TimeSpan parsedTimeSpan);
        return parsedTimeSpan;
    }
}