using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Generators.Models;

public class InternalType:ITransversable
{
    public InternalType(
        string? name = default,
        string? @namespace = default,
        string? summary = default,
        XType? reference = default,
        string? stereotype = default,
        List<InternalType>? genericArguments = default,
        List<string>? dependencies = default)
    {
        Name = name ?? string.Empty;
        Stereotype = stereotype ?? string.Empty;
        Namespace = @namespace ?? string.Empty;
        Summary = summary ?? string.Empty;
        Reference = reference;
        GenericArguments = genericArguments ?? new List<InternalType>();
        Dependencies = dependencies ?? new List<string>();
        Usings = Usings ?? new List<InternalType>();
    }

    public string Name { get; protected set; }
    public string Summary { get; protected set; }
    public string Namespace { get; protected set; }
    public XType? Reference { get; protected set; }
    public string Stereotype { get; protected set; }
    public List<InternalType> GenericArguments { get; protected set; }
    public List<string> Dependencies { get; protected set; }
    public List<InternalType> Usings { get; protected set; }


    public InternalType Set(
        string? name = default,
        string? @namespace = default,
        string? summary = default,
        XType? reference = default,
        string? stereotype = default)
    {
        Name = name ?? Name;
        Namespace = @namespace ?? Namespace;
        Summary = summary ?? Summary;
        Reference = reference ?? Reference;
        Stereotype = stereotype ?? Stereotype;
        return this;
    }
}