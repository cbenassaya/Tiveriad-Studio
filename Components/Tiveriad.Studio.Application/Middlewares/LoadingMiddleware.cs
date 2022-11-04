using Microsoft.Extensions.Logging;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class LoadingMiddleware : IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>
{
    private readonly ILoaderService _loaderService;
    private readonly IParserService _parserService;
    private readonly ILogger<LoadingMiddleware> _logger;

    public LoadingMiddleware(ILoaderService loaderService, IParserService parserService, ILogger<LoadingMiddleware> logger)
    {
        _loaderService = loaderService;
        _parserService = parserService;
        _logger = logger;
    }

    public Task Run(PipelineContext context, PipelineModel model)
    {
        var task = _loaderService.GetStreamAsync(context.Configuration.InputPath);
        using var stream = task.Result;
        model.Project = _parserService.Parse(stream);
        return Task.CompletedTask;
    }
}