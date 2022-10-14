using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Net.InternalTypes;

public class NamespaceProcessor :
    AbstractProcessor<InternalType, InternalType>
{
    private readonly IList<string> _dependencies = new List<string>();

    private NamespaceProcessor()
    {
    }

    public static void UpdateDependencies(InternalType type)
    {
        var namespaceProcessor = new NamespaceProcessor();
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
        return !string.IsNullOrEmpty(value.Namespace);
    }

    protected override void DoApply(InternalType value)
    {
        _dependencies.Add(value.Namespace);
    }
}