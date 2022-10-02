namespace Tiveriad.Studio.Generators.Models;

/// <summary>
///     Represents the various types of C# structure members.
/// </summary>
public enum MemberType
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    Interface,
    Class,
    Struct,
    Enum,
    EnumMember,
    Field,
    Property,
    UsingStatement,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}