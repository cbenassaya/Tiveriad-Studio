using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Application.Middlewares;

public class AddTypesMiddleware : IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>
{
    public void Run(PipelineContext context, PipelineModel model)
    {
        XDataTypes.Types.ForEach(x => model.TypeLoader.Add(x));
        XComplexTypes.Types.ForEach(x => model.TypeLoader.Add(x));
    }
}