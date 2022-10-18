namespace Tiveriad.Studio.Generators.Models;

public class NamedElement
{
}

public class Class : InternalType,ITransversable
{
    public Class(
        AccessModifier accessModifier,
        bool? isStatic = false,
        string? name = default,
        string? summary = default,
        InternalType? inheritedClass = default,
        List<InternalType>? implementedInterfaces = default,
        List<Attribute>? attributes = default,
        List<Field>? fields = default,
        List<Property>? properties = default,
        List<Method>? methods = default,
        List<TypeParameter>? typeParameters = default)
    {
        AccessModifier = accessModifier;
        IsStatic = isStatic ?? false;
        Name = name ?? string.Empty;
        Summary = summary ?? string.Empty;
        InheritedClass = inheritedClass;
        ImplementedInterfaces = implementedInterfaces ?? new List<InternalType>();
        Attributes = attributes ?? new List<Attribute>();
        Fields = fields ?? new List<Field>();
        Properties = properties ?? new List<Property>();
        Methods = methods ?? new List<Method>();
        TypeParameters = typeParameters ?? new List<TypeParameter>();
    }

    public AccessModifier AccessModifier { get; private set; }

    public bool IsStatic { get; private set; }

    public InternalType? InheritedClass { get; private set; }
    public List<InternalType> ImplementedInterfaces { get; set; }
    public List<Attribute> Attributes { get; set; }
    public List<Field> Fields { get; set; }
    public List<Property> Properties { get; set; }
    public List<Method> Methods { get; set; }
    public List<TypeParameter> TypeParameters { get; set; }


    public Class Set(
        AccessModifier? accessModifier = default,
        bool? isStatic = default,
        string? name = default,
        string? summary = default,
        InternalType? inheritedClass = default)

    {
        AccessModifier = accessModifier ?? AccessModifier;
        IsStatic = isStatic ?? IsStatic;
        Name = name ?? Name;
        Summary = summary ?? Summary;
        InheritedClass = inheritedClass ?? InheritedClass;
        return this;
    }

    public static Class With(
        AccessModifier accessModifier = AccessModifier.Public,
        bool? isStatic = false,
        string? name = default,
        string? summary = default,
        InternalType? inheritedClass = default)
    {
        return new Class(
            accessModifier,
            isStatic,
            name,
            summary,
            inheritedClass);
    }
}