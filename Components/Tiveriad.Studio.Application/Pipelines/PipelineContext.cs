using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using Tiveriad.Pipelines;

namespace Tiveriad.Studio.Application.Pipelines;

public class PipelineContext: IPipelineContext<PipelineConfiguration>
{
    public PipelineContext(PipelineConfiguration configuration, CancellationToken cancellationToken )
    {
        CancellationToken = cancellationToken;
        Configuration = configuration;
    }
    
    public CancellationToken CancellationToken { get; }
    public PipelineConfiguration Configuration { get; }

    public dynamic Properties { get; } = new ExpandoObject();
}



