using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Application.Pipelines;

public class PipelineModel
{
    public string InputPath { get; set; } = string.Empty;
   
    public XProject Project { get; set; }
    
}