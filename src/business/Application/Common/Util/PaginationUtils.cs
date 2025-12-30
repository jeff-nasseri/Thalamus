namespace Application.Common.Util;

/// <summary>
/// Provides utility methods for pagination calculations.
/// </summary>
public abstract class PaginationUtils
{
    /// <summary>
    /// Calculates the number of items to skip for pagination.
    /// </summary>
    /// <param name="skip">Output parameter for the number of items to skip.</param>
    /// <param name="perPage">Number of items per page.</param>
    /// <param name="page">The page number (1-based). If null, returns perPage as skip value.</param>
    public static void Paginate(out int skip, int perPage, int? page = null)
    {
        if (page == null)
        {
            skip = perPage;
            return;
        }

        skip = (page.Value - 1) * perPage;
    }
}