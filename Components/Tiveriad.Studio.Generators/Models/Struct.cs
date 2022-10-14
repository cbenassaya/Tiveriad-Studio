namespace Tiveriad.Studio.Generators.Models;

public class Struct : InternalType
{
    public Struct(
        AccessModifier accessModifier,
        string? name = default,
        string? summary = default,
        List<Interface>? implementedInterfaces = default,
        List<Field>? fields = default,
        List<Property>? properties = default,
        List<TypeParameter>? typeParameters = default)
    {
        AccessModifier = accessModifier;
        Name = name ?? string.Empty;
        Summary = summary ?? string.Empty;
        ImplementedInterfaces = implementedInterfaces ?? new List<Interface>();
        Fields = fields ?? new List<Field>();
        Properties = properties ?? new List<Property>();
        TypeParameters = typeParameters ?? new List<TypeParameter>();
    }

    public AccessModifier AccessModifier { get; private set; }

    public string Name { get; private set; }

    public string Summary { get; private set; }

    public List<Interface> ImplementedInterfaces { get; }

    public List<Field> Fields { get; }

    public List<Property> Properties { get; }

    public List<TypeParameter> TypeParameters { get; }

    public Struct Set(
        AccessModifier? accessModifier = default,
        string? name = default,
        string? summary = default)
    {
        AccessModifier = accessModifier ?? AccessModifier;
        Name = name ?? Name;
        Summary = summary ?? Summary;
        return this;
    }

    public static Struct With(
        AccessModifier accessModifier = AccessModifier.Public,
        string? name = default,
        string? summary = default)
    {
        return new Struct(accessModifier, name, summary);
    }
}