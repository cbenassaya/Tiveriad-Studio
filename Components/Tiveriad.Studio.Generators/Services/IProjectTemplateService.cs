using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Projects;

namespace Tiveriad.Studio.Generators.Services;

public interface IProjectTemplateService<T,P>
{
    public string GetItemPath(T internalType);
    public IEnumerable<P> GetProjects(XProject project);
    public IEnumerable<Dependency> GetDependencies(P project);
    public IEnumerable<string> GetReferences(P project);
    string GetLayer(string stereotype);
}