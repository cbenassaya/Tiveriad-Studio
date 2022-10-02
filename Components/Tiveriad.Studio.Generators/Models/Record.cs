using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Record : InternalType
{
    public Record(
        AccessModifier accessModifier,
        bool isStatic = false,
        Option<string> name = default,
        Option<string> summary = default,
        Option<List<InternalType>> implementedInterfaces = default,
        Option<List<Field>> fields = default,
        Option<List<Parameter>> parameters = default,
        Option<List<Property>> properties = default,
        Option<List<TypeParameter>> typeParameters = default)
    {
        AccessModifier = accessModifier;
        IsStatic = isStatic;
        Name = name;
        Summary = summary;
        ImplementedInterfaces = implementedInterfaces.ValueOr(new List<InternalType>());
        Fields = fields.ValueOr(new List<Field>());
        Parameters = parameters.ValueOr(new List<Parameter>());
        Properties = properties.ValueOr(new List<Property>());
        TypeParameters = typeParameters.ValueOr(new List<TypeParameter>());
    }

    public AccessModifier AccessModifier { get; private set; }

    public bool IsStatic { get; private set; }

    public List<InternalType> ImplementedInterfaces { get; set; }

    public List<Field> Fields { get; set; }

    public List<Parameter> Parameters { get; set; }

    public List<Property> Properties { get; set; }

    public List<TypeParameter> TypeParameters { get; set; }

    public Record Set(
        Option<AccessModifier> accessModifier = default,
        Option<bool> isStatic = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<Class> inheritedClass = default)

    {
        AccessModifier = accessModifier.ValueOr(AccessModifier);
        IsStatic = isStatic.ValueOr(IsStatic);
        Name = name.Else(Name);
        Summary = summary.Else(Summary);
        return this;
    }

    public static Record With(
        AccessModifier accessModifier = AccessModifier.Public,
        bool isStatic = false,
        Option<string> name = default,
        Option<string> summary = default)
    {
        return new(
            accessModifier,
            isStatic,
            name,
            summary);
    }
}