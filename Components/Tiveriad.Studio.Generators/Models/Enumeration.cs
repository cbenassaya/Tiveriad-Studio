namespace Tiveriad.Studio.Generators.Models;

public class Enumeration : InternalType
{
    public Enumeration(
        AccessModifier accessModifier,
        string? name = default,
        string? summary = default,
        bool? isFlag = default,
        List<EnumerationMember>? members = default)
    {
        AccessModifier = accessModifier;
        Name = name ?? string.Empty;
        Summary = summary ?? string.Empty;
        IsFlag = isFlag ?? false;
        Members = members ?? new List<EnumerationMember>();
    }

    public AccessModifier AccessModifier { get; private set; }

    public bool IsFlag { get; private set; }

    public List<EnumerationMember> Members { get; }

    public Enumeration Set(
        AccessModifier? accessModifier = default,
        string? name = default,
        string? summary = default,
        bool? isFlag = default)
    {
        AccessModifier = accessModifier ?? AccessModifier;
        IsFlag = isFlag ?? IsFlag;
        Name = name ?? Name;
        Summary = summary ?? Summary;
        return this;
    }

    public static Enumeration With(
        AccessModifier accessModifier = AccessModifier.Public,
        string? name = default,
        string? summary = default,
        bool? isFlag = default,
        List<EnumerationMember>? members = default)
    {
        return new Enumeration(
            accessModifier,
            name,
            summary,
            isFlag,
            members);
    }
}