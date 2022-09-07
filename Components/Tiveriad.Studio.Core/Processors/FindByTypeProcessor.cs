using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Processors;

public class FindByTypeProcessor<T> : AbstractProcessor<XElementBase, XNamedElement>, IProcessor
{
    public IList<T> Values { get; } = new List<T>();

    protected override bool ApplyIf(XElementBase value)
    {
        return value is T;
    }

    protected override void DoApply(XElementBase value)
    {
        if (value is T item)
            Values.Add(item);
    }
}