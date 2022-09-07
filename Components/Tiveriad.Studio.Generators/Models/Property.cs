using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Property
{
    public const string AutoGetterSetter = "@auto";

    public Property(
        AccessModifier accessModifier,
        AccessModifier setterAccessModifier,
        bool isStatic = false,
        Option<InternalType> type = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<string> defaultValue = default,
        Option<string> getter = default,
        Option<string> setter = default,
        Option<List<TypeParameter>> typeParameters = default,
        Option<List<Attribute>> attributes = default)
    {
        AccessModifier = accessModifier;
        SetterAccessModifier = setterAccessModifier;
        IsStatic = isStatic;
        Type = type;
        Name = name;
        Summary = summary;
        DefaultValue = defaultValue;
        Getter = getter;
        Setter = setter;
        TypeParameters = typeParameters.ValueOr(new List<TypeParameter>());
        Attributes = attributes.ValueOr(new List<Attribute>());
    }

    public AccessModifier AccessModifier { get;  private set; }

    public AccessModifier SetterAccessModifier { get;  private set; }

    public bool IsStatic { get;  private set; }

    public Option<InternalType> Type { get;  private set; }

    public Option<string> Name { get;  private set; }

    public Option<string> Summary { get;  private set; }

    public Option<string> DefaultValue { get;  private set; }

    public Option<string> Getter { get;  private set; }

    public Option<string> Setter { get;  private set; }

    public List<TypeParameter> TypeParameters { get;  private set; }
        
    public List<Attribute> Attributes { get;  private set; }


    public Property Set(
        Option<AccessModifier> accessModifier = default,
        Option<AccessModifier> setterAccessModifier = default,
        Option<bool> isStatic = default,
        Option<InternalType> type = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<string> defaultValue = default,
        Option<Option<string>> getter = default,
        Option<Option<string>> setter = default)
    {
        AccessModifier = accessModifier.ValueOr(AccessModifier);
        IsStatic = isStatic.ValueOr(IsStatic);
        Name = name.Else(Name);
        Type = type.Else(Type);
        Summary = summary.Else(Summary);
        SetterAccessModifier = setterAccessModifier.ValueOr(SetterAccessModifier);
        Getter = getter.ValueOr(Getter);
        Setter = setter.ValueOr(Setter);
        DefaultValue = defaultValue.Else(DefaultValue);
        return this;
    }
    
    public  static Property With(
        AccessModifier accessModifier = AccessModifier.Public,
        AccessModifier setterAccessModifier = AccessModifier.Public,
        bool isStatic = false,
        Option<InternalType> type = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<string> defaultValue = default,
        Option<string> getter = default,
        Option<string> setter = default) =>
        new(
            accessModifier: accessModifier,
            setterAccessModifier: setterAccessModifier,
            isStatic: isStatic,
            type: type,
            name: name,
            summary: summary,
            defaultValue:defaultValue,
            getter:getter,
            setter:setter);
}