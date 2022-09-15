using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class LoadingMiddleware:IMiddleware<PipelineModel,PipelineContext,PipelineConfiguration>
{
    private readonly ILoaderService _loaderService;
    private readonly IParserService _parserService;

    public LoadingMiddleware(ILoaderService loaderService, IParserService parserService)
    {
        _loaderService = loaderService;
        _parserService = parserService;
    }

    public void Run(PipelineContext context, PipelineModel model)
    {
        var task = _loaderService.GetStreamAsync(model.InputPath);
        using var stream = task.Result;
        model.Project = _parserService.Parse(stream);
    }
}