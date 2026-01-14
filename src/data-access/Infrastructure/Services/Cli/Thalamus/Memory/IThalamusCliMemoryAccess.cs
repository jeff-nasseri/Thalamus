using ErrorHandling;
using Infrastructure.Services.Cli.Base;

namespace Infrastructure.Services.Cli.Thalamus.Memory;

/// <summary>
/// Provides API access for the CLI to retrieve and export Thalamus application memory.
/// Supports filtering and exporting memory data in various formats.
/// </summary>
public interface IThalamusCliMemoryAccess : ICliService
{
    /// <summary>
    /// Retrieves memory data from the Thalamus application.
    /// Supports filtering by keywords, title, or retrieving all memory entries.
    /// </summary>
    /// <param name="args">Command arguments including filter options: 'all', 'keywords', or 'title'. Empty or '--all' returns all entries.</param>
    /// <returns>A response containing the filtered memory data.</returns>
    Task<Response> GetMemoryAsync(string[] args);

    /// <summary>
    /// Exports Thalamus memory data to a file.
    /// Supports multiple export formats (JSON, etc.) and custom file paths.
    /// Can be combined with filter options to export specific memory entries.
    /// </summary>
    /// <param name="args">Command arguments including 'format' (JSON, or empty for all formats) and optional 'path' (defaults to default path if empty).</param>
    /// <returns>A response indicating the result of the export operation.</returns>
    Task<Response> ExportMemoryAsync(string[] args);
}