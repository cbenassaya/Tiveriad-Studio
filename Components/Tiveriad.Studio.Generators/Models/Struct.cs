using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Struct : InternalType
{
    public Struct(
        AccessModifier accessModifier,
        Option<string> name = default,
        Option<string> summary = default,
        Option<List<Interface>> implementedInterfaces = default,
        Option<List<Field>> fields = default,
        Option<List<Property>> properties = default,
        Option<List<TypeParameter>> typeParameters = default)
    {
        AccessModifier = accessModifier;
        Name = name;
        Summary = summary;
        ImplementedInterfaces = implementedInterfaces.ValueOr(new List<Interface>());
        Fields = fields.ValueOr(new List<Field>());
        Properties = properties.ValueOr(new List<Property>());
        TypeParameters = typeParameters.ValueOr(new List<TypeParameter>());
    }

    public AccessModifier AccessModifier { get; private set; }

    public Option<string> Name { get; private set; }

    public Option<string> Summary { get; private set; }

    public List<Interface> ImplementedInterfaces { get; }

    public List<Field> Fields { get; }

    public List<Property> Properties { get; }

    public List<TypeParameter> TypeParameters { get; }

    public Struct Set(
        Option<AccessModifier> accessModifier = default,
        Option<string> name = default,
        Option<string> summary = default)
    {
        AccessModifier = accessModifier.ValueOr(AccessModifier);
        Name = name.Else(Name);
        Summary = summary.Else(Summary);
        return this;
    }

    public static Struct With(
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