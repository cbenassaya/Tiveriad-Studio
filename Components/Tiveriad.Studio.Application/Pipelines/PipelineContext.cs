using System.Dynamic;
using Tiveriad.Pipelines;

namespace Tiveriad.Studio.Application.Pipelines;

public class PipelineContext : IPipelineContext<PipelineConfiguration>
{
    public PipelineContext(PipelineConfiguration configuration, CancellationToken cancellationToken)
    {
        CancellationToken = cancellationToken;
        Configuration = configuration;
    }

    public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

    public CancellationToken CancellationToken { get; }
    public PipelineConfiguration Configuration { get; }
}