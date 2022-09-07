using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Field
{
    public Field(
        AccessModifier accessModifier,
        bool isReadonly= false,
        bool initializeFromConstructor= false,
        Option<InternalType> type = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<List<TypeParameter>> typeParameters = default)
    {
        AccessModifier = accessModifier;
        IsReadonly= isReadonly;
        InitializeFromConstructor= initializeFromConstructor;
        Type = type;
        Name = name;
        Summary = summary;
        TypeParameters = typeParameters.ValueOr(new List<TypeParameter>());
    }

    public AccessModifier AccessModifier { get;  private set; }

    public bool IsReadonly{ get;  private set; }
    public bool InitializeFromConstructor{ get;  private set; }

    public Option<InternalType> Type { get;  private set; }

    public Option<string> Name { get;  private set; }

    public Option<string> Summary { get;  private set; }

    public List<TypeParameter> TypeParameters { get;  private set; }

    
    public  Field Set(
        Option<AccessModifier> accessModifier = default,
        Option<bool> isReadonly = default,
        Option<bool> initializeFromConstructor = default,
        Option<InternalType> type = default,
        Option<string> name = default,
        Option<string> summary = default)
    {
        AccessModifier = accessModifier.ValueOr(AccessModifier);
        Name = name.Else(Name);
        Type = type.Else(Type);
        Summary = summary.Else(Summary);
        IsReadonly = isReadonly.ValueOr(IsReadonly);
        InitializeFromConstructor = initializeFromConstructor.ValueOr(InitializeFromConstructor);
        return this;
    }
    
    
    public static Field With(
        AccessModifier accessModifier = AccessModifier.Public,
        bool isReadonly= default,
        bool initializeFromConstructor= default,
        Option<InternalType> type = default,
        Option<string> name = default,
        Option<string> summary = default) =>
        new Field(
            accessModifier: accessModifier,
            isReadonly: isReadonly,
            initializeFromConstructor:initializeFromConstructor,
            type:type,
            name: name,
            summary: summary);
}