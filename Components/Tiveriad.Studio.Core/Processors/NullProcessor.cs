using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Processors;

public class NullProcessor : AbstractProcessor<XElementBase, XNamedElement>, IProcessor
{
    protected override bool ApplyIf(XElementBase value)
    {
        return true;
    }

    protected override void DoApply(XElementBase value)
    {
        //Do nothing
    }
}