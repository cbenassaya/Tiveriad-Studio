using Microsoft.Extensions.DependencyInjection;
using Tiveriad.Pipelines;

namespace Tiveriad.Studio.Integration.Tests.Pipelines;

public class PipelineMiddlewareResolver : IMiddlewareResolver
{
    private readonly IServiceProvider _serviceProvider;

    public PipelineMiddlewareResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public object Resolve(Type type)
    {
        return _serviceProvider.GetRequiredService(type);
    }
}