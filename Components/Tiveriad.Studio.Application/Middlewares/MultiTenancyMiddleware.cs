using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class MultiTenancyMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IXTypeService _typeService;

    public MultiTenancyMiddleware(IXTypeService typeService)
    {
        _typeService = typeService;
    }

    public Task Run(PipelineContext context, PipelineModel model)
    {
        Traverse(model.Project);
        return Task.CompletedTask;
    }

    protected override bool ApplyIf(XElementBase value)
    {
        return value is XEntity { MultiTenancy: not null };
    }

    protected override void DoApply(XElementBase value)
    {
        var entity = value as XEntity;
        entity.Properties.Add(new XProperty
        {
            Type = XDataTypes.STRING,
            Name = "TenantId",
            Constraints = new List<XConstraint> { new RequiredConstraint() }
        });
    }
}