using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class ContextBuilderMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IXTypeService _typeService;

    public ContextBuilderMiddleware(IXTypeService typeService)
    {
        _typeService = typeService;
    }
    
    public void Run(PipelineContext context, PipelineModel model)
    {
        Traverse(model.Project);
    }
    
    protected override bool ApplyIf(XElementBase value)
    {
        return value is XComplexType;
    }

    protected override void DoApply(XElementBase value)
    {
        if (value is XComplexType complexType) _typeService.Add(complexType);
    }


}