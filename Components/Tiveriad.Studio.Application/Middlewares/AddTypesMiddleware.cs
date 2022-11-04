using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class AddTypesMiddleware : IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>
{
    private readonly IXTypeService _typeService;

    public AddTypesMiddleware(IXTypeService typeService)
    {
        _typeService = typeService;
    }

    public Task Run(PipelineContext context, PipelineModel model)
    {
        XDataTypes.Types.ForEach(x => _typeService.Add(x));
        XComplexTypes.Types.ForEach(x => _typeService.Add(x));
        return Task.CompletedTask;
    }
}