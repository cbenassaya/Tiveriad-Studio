using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Sources;

public class SourceItem
{
    public SourceItem(InternalType internalType, string source)
    {
        InternalType = internalType;
        Source = source;
    }

    public InternalType InternalType { get; private set; }
    public string Source { get; private set; }
}