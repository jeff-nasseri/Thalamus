namespace Application.Common.Attributes;

/// <summary>
///     Attribute to mark a handler as idempotent.
///     Idempotent handlers are executed only once, even if they are called multiple times.
///     Any handler marked <see cref="IdempotentHandlerAttribute" /> is safe to duplicated calls.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class IdempotentHandlerAttribute : Attribute
{
}