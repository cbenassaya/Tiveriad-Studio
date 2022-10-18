using System.Security.Cryptography.X509Certificates;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Net.InternalTypes;

public class NamespaceProcessor :
    AbstractProcessor<InternalType, ITransversable>
{
    private readonly IList<string> _dependencies;
    private readonly IList<InternalType> _internalTypes;

    private NamespaceProcessor(IList<string> dependencies, IList<InternalType> internalTypes)
    {
        _dependencies = dependencies;
        _internalTypes = internalTypes;
    }


    public static void UpdateDependencies(InternalType type, IList<InternalType> internalTypes)
    {
        var namespaceProcessor = new NamespaceProcessor( new List<string>(),internalTypes );
        namespaceProcessor.DoUpdateDependencies(type);
    }

    private void DoUpdateDependencies(InternalType type)
    {
        Traverse(type);

        _dependencies.Where(x => x != type.Namespace).ToList().ForEach(x =>
        {
            if (!type.Dependencies.Contains(x))
                type.Dependencies.Add(x);
        });
    }


    protected override bool ApplyIf(InternalType value)
    {
        return value is InternalType;
    }

    protected override void DoApply(InternalType value)
    {
        if (value.Reference != null)
        {
            var searchBy = value.Name.EndsWith("?") ? value.Name.Substring(0,value.Name.Length-1)  :value.Name;
            
            var reference = _internalTypes.FirstOrDefault(x =>
                x.Name == searchBy && x.Reference != null && x.Reference.Namespace.Equals(value.Reference.Namespace));

            if (reference != null)
                value.Set(@namespace:reference.Namespace);
        }
        if (!_dependencies.Contains(value.Namespace))
            _dependencies.Add(value.Namespace);
    }
}