namespace ErrorHandling.Enums;

/// <summary>
/// Defines handler codes used to prefix error codes for specific request handlers.
/// Handler codes are multiplied by 1000 and added to error codes to create unique composite codes.
/// </summary>
public enum HandlerCode
{
    /// <summary>
    ///     Handler code for the InitializeAgents command.
    /// </summary>
    InitializeAgents = 01_01_01,
    
    /// <summary>
    ///     Handler code for the InitializeMcp command.
    /// </summary>
    InitializeMcp = 01_02_01,
    
    /// <summary>
    ///     Handler code for the InitializeNode command.
    /// </summary>
    InitializeNode = 01_03_01,
}