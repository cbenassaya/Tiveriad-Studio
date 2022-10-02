using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Interface : InternalType
{
    public Interface(
        AccessModifier accessModifier,
        Option<string> name = default,
        Option<string> summary = default,
        Option<List<Interface>> extentedInterfaces = default,
        Option<List<Property>> properties = default,
        Option<List<TypeParameter>> typeParameters = default)
    {
        AccessModifier = accessModifier;
        Name = name;
        Summary = summary;
        ExtentedInterfaces = extentedInterfaces.ValueOr(new List<Interface>());
        Properties = properties.ValueOr(new List<Property>());
        TypeParameters = typeParameters.ValueOr(new List<TypeParameter>());
    }

    public AccessModifier AccessModifier { get; private set; }

    public List<Interface> ExtentedInterfaces { get; }

    public List<Property> Properties { get; }

    public List<TypeParameter> TypeParameters { get; }

    public Interface Set(
        Option<AccessModifier> accessModifier = default,
        Option<string> name = default,
        Option<string> summary = default)
    {
        AccessModifier = accessModifier.ValueOr(AccessModifier);
        Name = name.Else(Name);
        Summary = summary.Else(Summary);
        return this;
    }


    public static Interface With(
        AccessModifier accessModifier = AccessModifier.Public,
        Option<string> name = default,
        Option<string> summary = default)
    {
        return new(
            accessModifier,
            name,
            summary);
    }
}