using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Method
{
    public Method(
        AccessModifier accessModifier,
        bool isStatic = false,
        bool isAsync = false,
        bool isConstructor = false,
        Option<Class> parent = default,
        Option<InternalType> returnType = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<string> body = default,
        Option<List<Parameter>> parameters = default,
        Option<List<string>> baseCallParameters = default,
        Option<List<Attribute>> attributes = default)
    {
        AccessModifier = accessModifier;
        IsStatic = isStatic;
        IsAsync = isAsync;
        IsConstructor = isConstructor;
        Parent = parent;
        ReturnType = returnType;
        Name = name;
        Summary = summary;
        Body = body;
        Parameters = parameters.ValueOr(new List<Parameter>());
        BaseCallParameters = baseCallParameters.ValueOr(new List<string>());
        Attributes = attributes.ValueOr(new List<Attribute>());
    }

    public AccessModifier AccessModifier { get;  private set; }
    public bool IsStatic { get;  private set; }
    public bool IsConstructor { get;  private set; }
    public bool IsAsync { get;  private set; }
    public Option<Class> Parent { get;  private set; }
    public Option<InternalType> ReturnType { get;  private set; }
    public Option<string> Name { get;  protected set; }
    public Option<string> Summary { get;  private set; }
    public Option<string> Body { get;  private set; }
    public List<Parameter> Parameters { get;  private set; }
    public List<Attribute> Attributes { get;  private set; }
    public List<string> BaseCallParameters { get;  private set; }

    public Method Set(
        Option<AccessModifier> accessModifier = default,
        Option<bool> isStatic = default,
        Option<bool> isAsync = default,
        Option<bool> isConstructor = default,
        Option<Class> parent = default,
        Option<InternalType> returnType = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<string> body = default,
        Option<List<Parameter>> parameters = default,
        Option<List<Attribute>> attributes = default,
        Option<List<string>> baseCallParameters = default)
    {
        AccessModifier = accessModifier.ValueOr(AccessModifier);
        IsStatic = isStatic.ValueOr(IsStatic);
        IsAsync = isAsync.ValueOr(IsAsync);
        IsConstructor = isConstructor.ValueOr(IsConstructor);
        Parent = parent.Else(Parent);
        ReturnType = returnType.Else(ReturnType);
        Name = name.Else(Name);
        Summary = summary.Else(Summary);
        Body = body.Else(Body);
        Parameters = parameters.ValueOr(Parameters);
        BaseCallParameters = baseCallParameters.ValueOr(BaseCallParameters);
        Attributes = attributes.ValueOr(Attributes);
        return this;
    }
    
    public static Method With(
        AccessModifier accessModifier = AccessModifier.Public,
        bool isStatic = false,
        bool isAsync = false,
        bool isConstructor = false,
        Option<Class> parent = default,
        Option<InternalType> returnType = default,
        Option<string> name = default,
        Option<string> summary = default,
        Option<string> body = default) =>
        new Method(
            accessModifier: accessModifier,
            isStatic: isStatic,
            isAsync: isAsync,
            isConstructor:isConstructor,
            parent: parent,
            returnType:returnType,
            name: name,
            summary: summary,
            body:body);
}