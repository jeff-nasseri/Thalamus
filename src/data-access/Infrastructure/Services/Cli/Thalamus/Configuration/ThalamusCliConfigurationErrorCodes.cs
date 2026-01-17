using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Infrastructure.Services.Cli.Thalamus.Configuration;

/// <summary>
///     Error codes for Thalamus CLI configuration operations.
/// </summary>
[HandlerCode(HandlerCode.InitializeAgents)] // Using a handler code as base
public enum ThalamusCliConfigurationErrorCodes
{
    /// <summary>
    ///     Indicates that a configuration file was not found.
    /// </summary>
    [ErrorType(BackendErrorType.PlatformFailure)]
    ConfigurationFileNotFound = 1,

    /// <summary>
    ///     Indicates that a configuration file contains no data.
    /// </summary>
    [ErrorType(BackendErrorType.BusinessLogic)]
    EmptyConfiguration = 2,

    /// <summary>
    ///     Indicates an unknown error occurred during configuration initialization.
    /// </summary>
    [ErrorType(BackendErrorType.UnKnownException)]
    UnknownInitializationError = 99
}