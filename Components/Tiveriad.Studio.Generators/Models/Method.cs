namespace Tiveriad.Studio.Generators.Models;

public class Method
{
    public Method(
        AccessModifier accessModifier,
        bool? isStatic = false,
        bool? isAsync = false,
        bool? isConstructor = false,
        Class? parent = default,
        InternalType? returnType = default,
        string? name = default,
        string? summary = default,
        string? body = default,
        List<Parameter>? parameters = default,
        List<string>? baseCallParameters = default,
        List<Attribute>? attributes = default)
    {
        AccessModifier = accessModifier;
        IsStatic = isStatic ?? false;
        IsAsync = isAsync ?? false;
        IsConstructor = isConstructor ?? false;
        Parent = parent;
        ReturnType = returnType;
        Name = name ?? string.Empty;
        Summary = summary ?? string.Empty;
        Body = body ?? string.Empty;
        Parameters = parameters ?? new List<Parameter>();
        BaseCallParameters = baseCallParameters ?? new List<string>();
        Attributes = attributes ?? new List<Attribute>();
    }

    public AccessModifier AccessModifier { get; private set; }
    public bool IsStatic { get; private set; }
    public bool IsConstructor { get; private set; }
    public bool IsAsync { get; private set; }
    public Class? Parent { get; private set; }
    public InternalType? ReturnType { get; private set; }
    public string Name { get; protected set; }
    public string Summary { get; private set; }
    public string Body { get; private set; }
    public List<Parameter> Parameters { get; private set; }
    public List<Attribute> Attributes { get; private set; }
    public List<string> BaseCallParameters { get; private set; }

    public Method Set(
        AccessModifier? accessModifier = default,
        bool? isStatic = default,
        bool? isAsync = default,
        bool? isConstructor = default,
        Class? parent = default,
        InternalType? returnType = default,
        string? name = default,
        string? summary = default,
        string? body = default,
        List<Parameter>? parameters = default,
        List<Attribute>? attributes = default,
        List<string>? baseCallParameters = default)
    {
        AccessModifier = accessModifier ?? AccessModifier;
        IsStatic = isStatic ?? IsStatic;
        IsAsync = isAsync ?? IsAsync;
        IsConstructor = isConstructor ?? IsConstructor;
        Parent = parent ?? Parent;
        ReturnType = returnType ?? ReturnType;
        Name = name ?? Name;
        Summary = summary ?? Summary;
        ;
        Body = body ?? Body;
        Parameters = parameters ?? Parameters;
        BaseCallParameters = baseCallParameters ?? BaseCallParameters;
        Attributes = attributes ?? Attributes;
        return this;
    }

    public static Method With(
        AccessModifier accessModifier = AccessModifier.Public,
        bool? isStatic = false,
        bool? isAsync = false,
        bool? isConstructor = false,
        Class? parent = default,
        InternalType? returnType = default,
        string? name = default,
        string? summary = default,
        string? body = default)
    {
        return new Method(
            accessModifier,
            isStatic,
            isAsync,
            isConstructor,
            parent,
            returnType,
            name,
            summary,
            body);
    }
}