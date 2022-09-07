using Optional;
using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Generators.Models;

public class InternalType
{
    public InternalType(
        Option<string> name = default, 
        Option<string> @namespace = default, 
        Option<string> summary = default, 
        Option<XType> reference = default,
        Option<List<InternalType>> genericArguments = default,
        Option<List<string>> dependencies = default)
    {
        Name = name;
        Namespace = @namespace;
        Summary = summary;
        Reference = reference;
        GenericArguments = genericArguments.ValueOr(new List<InternalType>());
        Dependencies = dependencies.ValueOr(new List<string>());
    }
    
    
    public InternalType Set (Option<string> name = default, Option<string> @namespace = default, Option<string> summary = default, Option<XType> reference = default)
    {
        Name = name.Else(Name);
        Namespace = @namespace.Else(Namespace);
        Summary = summary.Else(Summary);
        Reference = reference.Else(Reference);
        return this;
    }
    
    public Option<string> Name { get;  protected set; }
    public Option<string> Summary { get; protected set;}
    public Option<string> Namespace { get;  protected set;}
    public Option<XType> Reference { get;  protected set;}
    public List<InternalType> GenericArguments { get;  protected set;}
    public List<string> Dependencies { get;  protected set;}
}


