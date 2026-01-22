namespace Application.Common.Quee;

/// <summary>
/// Interface for pattern matching on queue names to support dynamic routing
/// and subscription patterns.
/// </summary>
public interface IQueuePatternMatcher
{
    /// <summary>
    /// Determines whether a queue name matches a given pattern.
    /// </summary>
    /// <param name="queueName">The queue name to test.</param>
    /// <param name="pattern">The pattern to match against.</param>
    /// <returns>True if the queue name matches the pattern; otherwise, false.</returns>
    bool Matches(string queueName, string pattern);

    /// <summary>
    /// Extracts parameter values from a queue name based on a pattern.
    /// </summary>
    /// <param name="queueName">The queue name to extract parameters from.</param>
    /// <param name="pattern">The pattern containing parameter placeholders.</param>
    /// <returns>A collection of extracted parameter values.</returns>
    IEnumerable<string> ExtractParameters(string queueName, string pattern);
}
