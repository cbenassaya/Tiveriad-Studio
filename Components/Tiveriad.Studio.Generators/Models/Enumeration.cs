using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Enumeration:InternalType
{
    public Enumeration(
        AccessModifier accessModifier,
        Option<string> name = default,
        Option<string> summary = default,
        bool isFlag = default,
        Option<List<EnumerationMember>> members = default)
    {
        AccessModifier = accessModifier;
        Name = name;
        Summary = summary;
        IsFlag = isFlag;
        Members = members.ValueOr(new List<EnumerationMember>());
    }

    public AccessModifier AccessModifier { get;  private set; }

    public bool IsFlag { get;  private set; }

    public List<EnumerationMember> Members { get;  private set; }

    public  Enumeration Set(
        Option<AccessModifier> accessModifier = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<bool> isFlag = default)
    {
        AccessModifier = accessModifier.ValueOr(AccessModifier);
        IsFlag = isFlag.ValueOr(IsFlag);
        Name = name.Else(Name);
        Summary = summary.Else(Summary);
        return this;
    }
    
    public static Enumeration With(
        AccessModifier accessModifier = AccessModifier.Public,
        Option<string> name = default,
        Option<string> summary = default,
        bool isFlag = default,
        Option<List<EnumerationMember>> members = default) =>
        new Enumeration(
            accessModifier: accessModifier,
            name: name,
            summary: summary,
            isFlag: isFlag,
            members:members);
}