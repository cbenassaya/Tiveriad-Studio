using Optional;

namespace Tiveriad.Studio.Generators.Models;


public class Class : InternalType
{
    public Class(
        AccessModifier accessModifier,
        bool isStatic = false,
        Option<string> name = default,
        Option<string> summary = default,
        Option<InternalType> inheritedClass = default,
        Option<List<InternalType>> implementedInterfaces = default,
        Option<List<Attribute>> attributes = default,
        Option<List<Field>> fields = default,
        Option<List<Property>> properties = default,
        Option<List<Method>> methods = default,
        Option<List<TypeParameter>> typeParameters = default)
    {
        AccessModifier = accessModifier;
        IsStatic = isStatic;
        Name = name;
        Summary = summary;
        InheritedClass = inheritedClass;
        ImplementedInterfaces = implementedInterfaces.ValueOr(new List<InternalType>());
        Attributes = attributes.ValueOr(new List<Attribute>());
        Fields = fields.ValueOr(new List<Field>());
        Properties = properties.ValueOr(new List<Property>());
        Methods = methods.ValueOr(new List<Method>());
        TypeParameters = typeParameters.ValueOr(new List<TypeParameter>());
    }

    public AccessModifier AccessModifier { get;  private set; }

    public bool IsStatic { get;  private set; }

    public Option<InternalType> InheritedClass { get;  private set; }

    public List<InternalType> ImplementedInterfaces { get;   set; }
    public List<Attribute> Attributes { get;   set; }

    public List<Field> Fields { get;   set; }

    public List<Property> Properties { get;   set; }

    public List<Method> Methods { get;   set; }

    public List<TypeParameter> TypeParameters { get;   set; }

    
    public  Class Set(
        Option<AccessModifier> accessModifier = default,
        Option<bool> isStatic = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<InternalType> inheritedClass = default) 

    {
        AccessModifier = accessModifier.ValueOr(AccessModifier);
        IsStatic = isStatic.ValueOr(IsStatic);
        Name = name.Else(Name);
        Summary = summary.Else(Summary);
        InheritedClass = inheritedClass.Else(InheritedClass);
        return this;
    }

    public static Class With(
        AccessModifier accessModifier = AccessModifier.Public,
        bool isStatic = false,
        Option<string> name = default,
        Option<string> summary = default,
        Option<InternalType> inheritedClass = default)=> new Class(
            accessModifier: accessModifier,
            isStatic: isStatic,
            name: name,
            summary: summary,
            inheritedClass: inheritedClass);
}