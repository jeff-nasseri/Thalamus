using Domain.Common.BaseTypes;

namespace Domain.ValueObjects;

/// <summary>
///     Represents a message value object in a prompt, including the message content and token count.
/// </summary>
public class MessageValueObject : ValueObject, IValueObjectParser<MessageValueObject, string>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MessageValueObject" /> class.
    /// </summary>
    /// <param name="message">The message content.</param>
    public MessageValueObject(string message)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
        NumberOfTokens = ExtractNumberOfToken(message);
    }

    /// <summary>
    ///     Gets the message content.
    /// </summary>
    public string Message { get; }

    /// <summary>
    ///     Gets the estimated number of tokens in the message.
    /// </summary>
    public long NumberOfTokens { get; }

    /// <summary>
    ///     Attempts to parse a string into a MessageValueObject.
    /// </summary>
    /// <param name="input">The input string to parse.</param>
    /// <param name="valueObject">The parsed MessageValueObject if successful; otherwise, null.</param>
    /// <returns>True if parsing was successful; otherwise, false.</returns>
    public static bool TryParse(string input, out MessageValueObject? valueObject)
    {
        try
        {
            if (input == null)
            {
                valueObject = null;
                return false;
            }

            valueObject = new MessageValueObject(input);
            return true;
        }
        catch
        {
            valueObject = null;
            return false;
        }
    }

    /// <summary>
    ///     Parses a string into a MessageValueObject.
    /// </summary>
    /// <param name="input">The input string to parse.</param>
    /// <returns>The parsed MessageValueObject.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input is null.</exception>
    public static MessageValueObject Parse(string input)
    {
        if (!TryParse(input, out var valueObject) || valueObject == null)
            throw new ArgumentNullException(nameof(input),
                "Cannot parse null or invalid input into a MessageValueObject.");

        return valueObject;
    }

    /// <summary>
    ///     Extracts the number of tokens from the message.
    ///     This is a simple approximation based on whitespace splitting.
    ///     For production use, consider using a proper tokenizer like tiktoken.
    /// </summary>
    /// <param name="message">The message to analyze.</param>
    /// <returns>The estimated number of tokens.</returns>
    private long ExtractNumberOfToken(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return 0;

        // Simple approximation: split by whitespace and count words
        // A more accurate implementation would use a proper tokenizer
        var words = message.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // Rough estimate: average English word is ~1.3 tokens
        // This is a simplified calculation and should be replaced with actual tokenization
        return (long)(words.Length * 1.3);
    }

    /// <summary>
    ///     Gets the components that define the equality of the MessageValueObject.
    /// </summary>
    /// <returns>An enumerable of objects that represent the equality components.</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Message;
        yield return NumberOfTokens;
    }
}