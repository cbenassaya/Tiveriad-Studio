namespace Tiveriad.Studio.Generators.Models;

public class Field
{
    public Field(
        AccessModifier accessModifier,
        bool? isReadonly = false,
        bool? initializeFromConstructor = false,
        InternalType? type = default,
        string? name = default,
        string? summary = default,
        List<TypeParameter>? typeParameters = default)
    {
        AccessModifier = accessModifier;
        IsReadonly = isReadonly ?? false;
        InitializeFromConstructor = initializeFromConstructor ?? false;
        Type = type;
        Name = name ?? string.Empty;
        Summary = summary ?? string.Empty;
        TypeParameters = typeParameters ?? new List<TypeParameter>();
    }

    public AccessModifier AccessModifier { get; private set; }

    public bool IsReadonly { get; private set; }
    public bool InitializeFromConstructor { get; private set; }

    public InternalType? Type { get; private set; }

    public string Name { get; private set; }

    public string Summary { get; private set; }

    public List<TypeParameter> TypeParameters { get; }


    public Field Set(
        AccessModifier? accessModifier = default,
        bool? isReadonly = default,
        bool? initializeFromConstructor = default,
        InternalType? type = default,
        string? name = default,
        string? summary = default)
    {
        AccessModifier = accessModifier ?? AccessModifier;
        Name = name ?? Name;
        Type = type ?? Type;
        Summary = summary ?? Summary;
        IsReadonly = isReadonly ?? IsReadonly;
        InitializeFromConstructor = initializeFromConstructor ?? InitializeFromConstructor;
        return this;
    }


    public static Field With(
        AccessModifier accessModifier = AccessModifier.Public,
        bool? isReadonly = default,
        bool? initializeFromConstructor = default,
        InternalType? type = default,
        string? name = default,
        string? summary = default)
    {
        return new Field(
            accessModifier,
            isReadonly,
            initializeFromConstructor,
            type,
            name,
            summary);
    }
}