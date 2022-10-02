namespace Tiveriad.Studio.Generators.Models;

/// <summary>
///     Represents the access modifier of a C# structure.
/// </summary>
public enum AccessModifier
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    None,
    Private,
    Internal,
    Protected,
    Public,
    PrivateProtected,
    ProtectedInternal,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}