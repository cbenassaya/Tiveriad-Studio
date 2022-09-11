namespace Tiveriad.Studio.Generators.Projects;

public interface IProjectTemplateService
{
    public string GetPath(string stereotype);
    public IEnumerable<string> GetPaths();
}