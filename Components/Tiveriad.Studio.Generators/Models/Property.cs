namespace Tiveriad.Studio.Generators.Models;

public class Property:ITransversable
{
    public const string AutoGetterSetter = "@auto";

    public Property(
        AccessModifier accessModifier,
        AccessModifier setterAccessModifier,
        bool? isStatic = false,
        InternalType? type = default,
        string? name = default,
        string? summary = default,
        string? defaultValue = default,
        string? getter = default,
        string? setter = default,
        List<TypeParameter>? typeParameters = default,
        List<Attribute>? attributes = default)
    {
        AccessModifier = accessModifier;
        SetterAccessModifier = setterAccessModifier;
        IsStatic = isStatic ?? false;
        Type = type;
        Name = name ?? string.Empty;
        Summary = summary ?? string.Empty;
        DefaultValue = defaultValue ?? string.Empty;
        Getter = getter ?? string.Empty;
        Setter = setter ?? string.Empty;
        TypeParameters = typeParameters ?? new List<TypeParameter>();
        Attributes = attributes ?? new List<Attribute>();
    }

    public AccessModifier AccessModifier { get; private set; }

    public AccessModifier SetterAccessModifier { get; private set; }

    public bool IsStatic { get; private set; }

    public InternalType? Type { get; private set; }

    public string Name { get; private set; }

    public string Summary { get; private set; }

    public string DefaultValue { get; private set; }

    public string Getter { get; private set; }

    public string Setter { get; private set; }

    public List<TypeParameter> TypeParameters { get; }

    public List<Attribute> Attributes { get; }


    public Property Set(
        AccessModifier? accessModifier = default,
        AccessModifier? setterAccessModifier = default,
        bool? isStatic = default,
        InternalType? type = default,
        string? name = default,
        string? summary = default,
        string? defaultValue = default,
        string? getter = default,
        string? setter = default)
    {
        AccessModifier = accessModifier ?? AccessModifier;
        IsStatic = isStatic ?? IsStatic;
        Name = name ?? Name;
        Type = type ?? Type;
        ;
        Summary = summary ?? Summary;
        ;
        SetterAccessModifier = setterAccessModifier ?? SetterAccessModifier;
        Getter = getter ?? Getter;
        Setter = setter ?? Setter;
        DefaultValue = defaultValue ?? DefaultValue;
        return this;
    }

    public static Property With(
        AccessModifier accessModifier = AccessModifier.Public,
        AccessModifier setterAccessModifier = AccessModifier.Public,
        bool? isStatic = false,
        InternalType? type = default,
        string? name = default,
        string? summary = default,
        string? defaultValue = default,
        string? getter = default,
        string? setter = default)
    {
        return new Property(
            accessModifier,
            setterAccessModifier,
            isStatic,
            type,
            name,
            summary,
            defaultValue,
            getter,
            setter);
    }
}