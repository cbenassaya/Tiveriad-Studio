namespace Tiveriad.Studio.Generators.Models;

public class Interface : InternalType
{
    public Interface(
        AccessModifier? accessModifier,
        string? name = default,
        string? summary = default,
        List<Interface>? extentedInterfaces = default,
        List<Property>? properties = default,
        List<TypeParameter>? typeParameters = default)
    {
        AccessModifier = accessModifier ?? AccessModifier;
        Name = name ?? string.Empty;
        Summary = summary ?? string.Empty;
        ExtentedInterfaces = extentedInterfaces ?? new List<Interface>();
        Properties = properties ?? new List<Property>();
        TypeParameters = typeParameters ?? new List<TypeParameter>();
    }

    public AccessModifier AccessModifier { get; private set; }

    public List<Interface> ExtentedInterfaces { get; }

    public List<Property> Properties { get; }

    public List<TypeParameter> TypeParameters { get; }

    public Interface Set(
        AccessModifier? accessModifier = default,
        string? name = default,
        string? summary = default)
    {
        AccessModifier = accessModifier ?? AccessModifier;
        Name = name ?? Name;
        Summary = summary ?? Summary;
        ;
        return this;
    }


    public static Interface With(
        AccessModifier accessModifier = AccessModifier.Public,
        string? name = default,
        string? summary = default)
    {
        return new Interface(
            accessModifier,
            name,
            summary);
    }
}