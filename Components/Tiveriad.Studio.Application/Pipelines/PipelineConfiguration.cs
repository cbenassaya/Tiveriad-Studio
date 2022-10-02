using Tiveriad.Pipelines;

namespace Tiveriad.Studio.Application.Pipelines;

public class PipelineConfiguration : IPipelineConfiguration
{
    public string OutputPath { get; set; } = string.Empty;
}