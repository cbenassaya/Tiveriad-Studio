using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Generators.Services;

public interface IProjectTemplateService<T>
{
    public string GetPath(T internalType, XProject project);
}