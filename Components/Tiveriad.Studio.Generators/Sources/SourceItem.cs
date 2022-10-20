using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Sources;

public class SourceItem
{
    private string _source;
    private string _name;
    private string _directory;

    public string Source => _source;
    public string Name => _name;
    public string Directory => _directory;

    private SourceItem() { }

    public static SourceItem Init()
    {
        return new SourceItem();
    }
    public SourceItem WithSource(string source)
    {
        _source = source;
        return this;
    }
    
    public SourceItem WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public SourceItem WithDirectory(string directory)
    {
        _directory = directory;
        return this;
    }
    
    

}