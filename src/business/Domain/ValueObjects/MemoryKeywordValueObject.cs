using Domain.Common.BaseTypes;

namespace Domain.ValueObjects;

/// <summary>
/// Represents a memory keyword value object for indexing and retrieving related memories.
/// Each keyword is stored with linguistic components to enable efficient memory recall based on semantic similarity.
/// </summary>
public class MemoryKeywordValueObject : ValueObject, IValueObjectParser<MemoryKeywordValueObject, string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryKeywordValueObject"/> class.
    /// </summary>
    /// <param name="keyword">The keyword to store.</param>
    public MemoryKeywordValueObject(string keyword)
    {
        Keyword = keyword ?? throw new ArgumentNullException(nameof(keyword));
        Prefix = ExtractPrefix(keyword);
        Suffix = ExtractSuffix(keyword);
        Root = ExtractRoot(keyword);
        Phonemes = ExtractPhonemes(keyword);
    }

    /// <summary>
    /// Gets the keyword.
    /// </summary>
    public string Keyword { get; private set; }

    /// <summary>
    /// Gets the prefix of the keyword.
    /// </summary>
    private string Prefix { get; set; }

    /// <summary>
    /// Gets the suffix of the keyword.
    /// </summary>
    private string Suffix { get; set; }

    /// <summary>
    /// Gets the root of the keyword.
    /// </summary>
    private string Root { get; set; }

    /// <summary>
    /// Gets the phonemes of the keyword.
    /// </summary>
    private string Phonemes { get; set; }

    /// <summary>
    /// Attempts to parse multiple keywords from a sentence.
    /// </summary>
    /// <param name="sentence">The sentence containing keywords.</param>
    /// <param name="separator">The separator character to split keywords.</param>
    /// <returns>An enumerable of parsed MemoryKeywordValueObject instances.</returns>
    public static IEnumerable<MemoryKeywordValueObject> TryParseBulk(string sentence, char separator = ',')
    {
        if (string.IsNullOrWhiteSpace(sentence))
        {
            return Enumerable.Empty<MemoryKeywordValueObject>();
        }

        var keywords = sentence.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var result = new List<MemoryKeywordValueObject>();

        foreach (var keyword in keywords)
        {
            if (TryParse(keyword, out var valueObject) && valueObject != null)
            {
                result.Add(valueObject);
            }
        }

        return result;
    }

    /// <summary>
    /// Gets the components that define the equality of the MemoryKeywordValueObject.
    /// </summary>
    /// <returns>An enumerable of objects that represent the equality components.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Keyword;
        yield return Root;
    }

    /// <summary>
    /// Attempts to parse a string into a MemoryKeywordValueObject.
    /// </summary>
    /// <param name="input">The input string to parse.</param>
    /// <param name="valueObject">The parsed MemoryKeywordValueObject if successful; otherwise, null.</param>
    /// <returns>True if parsing was successful; otherwise, false.</returns>
    public static bool TryParse(string input, out MemoryKeywordValueObject? valueObject)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                valueObject = null;
                return false;
            }

            valueObject = new MemoryKeywordValueObject(input.Trim());
            return true;
        }
        catch
        {
            valueObject = null;
            return false;
        }
    }

    /// <summary>
    /// Parses a string into a MemoryKeywordValueObject.
    /// </summary>
    /// <param name="input">The input string to parse.</param>
    /// <returns>The parsed MemoryKeywordValueObject.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is invalid.</exception>
    public static MemoryKeywordValueObject Parse(string input)
    {
        if (!TryParse(input, out var valueObject) || valueObject == null)
        {
            throw new ArgumentException($"Cannot parse '{input}' into a MemoryKeywordValueObject.", nameof(input));
        }

        return valueObject;
    }

    private static string ExtractPrefix(string keyword)
    {
        // Basic prefix extraction - can be enhanced with linguistic rules
        return keyword.Length > 3 ? keyword.Substring(0, Math.Min(3, keyword.Length)) : string.Empty;
    }

    private static string ExtractSuffix(string keyword)
    {
        // Basic suffix extraction - can be enhanced with linguistic rules
        return keyword.Length > 3 ? keyword.Substring(Math.Max(0, keyword.Length - 3)) : string.Empty;
    }

    private static string ExtractRoot(string keyword)
    {
        // Basic root extraction - returns the keyword itself for now
        // Can be enhanced with stemming algorithms like Porter Stemmer
        return keyword.ToLowerInvariant();
    }

    private static string ExtractPhonemes(string keyword)
    {
        // Basic phoneme extraction - returns lowercase keyword
        // Can be enhanced with phonetic algorithms like Soundex or Metaphone
        return keyword.ToLowerInvariant();
    }
}