namespace Application.Common.Extensions;

/// <summary>
///     Provides base extension methods for common operations on collections and numeric types.
/// </summary>
public static class BaseExtension
{
    /// <summary>
    ///     Performs a Cartesian join on a collection, creating all possible pairs.
    /// </summary>
    /// <typeparam name="T">The output type.</typeparam>
    /// <typeparam name="Tg">The input collection element type.</typeparam>
    /// <param name="data">The collection to join.</param>
    /// <param name="make">Function to create output from two elements.</param>
    /// <returns>An enumerable of all possible pairs transformed by the make function.</returns>
    public static IEnumerable<T> JoinEach<T, Tg>(this IEnumerable<Tg> data, Func<Tg, Tg, T> make)
    {
        List<Tg> enumerable = data.ToList();
        return (from item in enumerable from item2 in enumerable select make(item, item2)).ToList();
    }

    /// <summary>
    ///     Asynchronously iterates over a collection, applying an async function to each element.
    /// </summary>
    /// <typeparam name="T">The input element type.</typeparam>
    /// <typeparam name="TOut">The output element type.</typeparam>
    /// <param name="data">The collection to iterate.</param>
    /// <param name="func">The async function to apply to each element.</param>
    /// <returns>A task representing the collection of results.</returns>
    public static async Task<IEnumerable<TOut>> ForEach<T, TOut>(this IEnumerable<T> data, Func<T, Task<TOut>> func)
    {
        List<TOut> list = new();

        foreach (var item in data)
        {
            var result = await func(item);
            list.Add(result);
        }

        return list;
    }

    /// <summary>
    ///     Executes an action for each element in the collection.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="data">The collection to iterate.</param>
    /// <param name="action">The action to execute for each element.</param>
    public static void ForEach<T>(this IEnumerable<T> data, Action<T> action)
    {
        foreach (var item in data) action(item);
    }

    /// <summary>
    ///     Executes an action a specified number of times.
    /// </summary>
    /// <param name="num">The number of times to execute the action.</param>
    /// <param name="f">The action to execute.</param>
    public static void For(this int num, Action f)
    {
        for (var i = 0; i < num; i++) f.Invoke();
    }
}