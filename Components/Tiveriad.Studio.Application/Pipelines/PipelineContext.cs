using Tiveriad.Pipelines;

namespace Tiveriad.Studio.Application.Pipelines;

public class PipelineContext: PipelineContextBase<PipelineConfiguration>
{
    public PipelineContext(PipelineConfiguration pipelineConfiguration, CancellationToken cancellationToken) : base(pipelineConfiguration, cancellationToken)
    {
    }
}