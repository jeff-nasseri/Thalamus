using System.Collections.ObjectModel;
using System.Text;

namespace Application.Common.Util;

/// <summary>
/// Provides utility methods for array operations.
/// </summary>
public abstract class ArrayUtils
{
    /// <summary>
    /// Converts a byte array to a hexadecimal string representation.
    /// </summary>
    /// <param name="arr">The byte array to convert.</param>
    /// <returns>A hexadecimal string representation of the byte array.</returns>
    public static string ArrayToString(ReadOnlyCollection<byte> arr)
    {
        StringBuilder s = new(arr.Count * 2);

        foreach (byte t in arr)
        {
            s.AppendFormat("{0:x2}", t);
        }

        return s.ToString();
    }
}