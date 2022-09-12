using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class LoadingMiddleware:IMiddleware<PipelineModel,PipelineContext,PipelineConfiguration>
{
    private readonly ILoaderService _loaderService;
    private readonly IParserService _parserService;

    public LoadingMiddleware(ILoaderService loaderService)
    {
        _loaderService = loaderService;
    }

    public void Run(PipelineContext context, PipelineModel model)
    {
        var middleware = async () =>
        {
            await using var stream = await _loaderService.GetStreamAsync(model.InputPath);
            model.Project = _parserService.Parse(stream);
        };
        middleware();
    }
}