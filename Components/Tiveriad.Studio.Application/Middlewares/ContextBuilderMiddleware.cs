using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Processors;

namespace Tiveriad.Studio.Application.Middlewares;

public class ContextBuilderMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private XTypeLoader _typeLoader;
    
    public void Run(PipelineContext context, PipelineModel model)
    {
        lock (_typeLoader)
        {
            _typeLoader = model.TypeLoader;
            Traverse(model.Project);
        }
    }
    
    protected override bool ApplyIf(XElementBase value)
    {
        return value is XComplexType;
    }

    protected override void DoApply(XElementBase value)
    {
        if (value is XComplexType complexType) _typeLoader.Add(complexType);
    }


}