namespace Tiveriad.Studio.Generators.Models;

public class Record : InternalType
{
    public Record(
        AccessModifier accessModifier,
        bool? isStatic = false,
        string? name = default,
        string? summary = default,
        List<InternalType>? implementedInterfaces = default,
        List<Field>? fields = default,
        List<Parameter>? parameters = default,
        List<Property>? properties = default,
        List<TypeParameter>? typeParameters = default)
    {
        AccessModifier = accessModifier;
        IsStatic = isStatic ?? false;
        Name = name ?? string.Empty;
        Summary = summary ?? string.Empty;
        ImplementedInterfaces = implementedInterfaces ?? new List<InternalType>();
        Fields = fields ?? new List<Field>();
        Parameters = parameters ?? new List<Parameter>();
        Properties = properties ?? new List<Property>();
        TypeParameters = typeParameters ?? new List<TypeParameter>();
    }

    public AccessModifier AccessModifier { get; private set; }

    public bool IsStatic { get; private set; }

    public List<InternalType> ImplementedInterfaces { get; set; }

    public List<Field> Fields { get; set; }

    public List<Parameter> Parameters { get; set; }

    public List<Property> Properties { get; set; }

    public List<TypeParameter> TypeParameters { get; set; }

    public Record Set(
        AccessModifier? accessModifier = default,
        bool? isStatic = default,
        string? name = default,
        string? summary = default)

    {
        AccessModifier = accessModifier ?? AccessModifier;
        IsStatic = isStatic ?? IsStatic;
        Name = name ?? Name;
        Summary = summary ?? Summary;
        return this;
    }

    public static Record With(
        AccessModifier accessModifier = AccessModifier.Public,
        bool? isStatic = false,
        string? name = default,
        string? summary = default)
    {
        return new Record(
            accessModifier,
            isStatic,
            name,
            summary);
    }
}