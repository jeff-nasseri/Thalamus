namespace Infrastructure.Common;

/// <summary>
/// Utility class for loading environment variables from .env files.
/// </summary>
public static class DotEnv
{
    /// <summary>
    /// Loads environment variables from a .env file and sets them in the current process.
    /// </summary>
    /// <param name="filePath">The path to the .env file to load.</param>
    /// <remarks>
    /// The method parses each line in the format KEY=VALUE and sets it as an environment variable.
    /// Lines starting with '#' are treated as comments and ignored.
    /// Empty lines and malformed entries are skipped.
    /// Values are automatically trimmed of surrounding quotes.
    /// </remarks>
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        foreach (string? line in File.ReadAllLines(filePath))
        {
            if (line.StartsWith("#"))
            {
                continue;
            }

            string[] parts = line.Split(
                '=',
                2,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            if (parts.Length != 2)
            {
                continue;
            }

            parts[1] = parts[1].Trim('"');

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}