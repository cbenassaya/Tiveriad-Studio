using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Processors;

namespace Tiveriad.Studio.Application.Pipelines;

public class PipelineModel
{
    public string InputPath { get; set; } = string.Empty;
    public XProject Project { get; set; } 
    
    public XTypeLoader TypeLoader { get; } = new XTypeLoader();
}