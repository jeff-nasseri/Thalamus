namespace Application.Common.Util;

/// <summary>
/// Provides utility methods for string operations.
/// </summary>
public static class StringUtils
{
    /// <summary>
    /// Generates a stream from a string content.
    /// </summary>
    /// <param name="s">The string to convert to a stream.</param>
    /// <returns>A stream containing the string content, positioned at the beginning.</returns>
    public static Stream GenerateStreamFromString(string s)
    {
        MemoryStream stream = new();
        StreamWriter writer = new(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}