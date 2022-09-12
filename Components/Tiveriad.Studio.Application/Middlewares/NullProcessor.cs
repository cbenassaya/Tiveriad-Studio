using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Processors;

namespace Tiveriad.Studio.Application.Middlewares;

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