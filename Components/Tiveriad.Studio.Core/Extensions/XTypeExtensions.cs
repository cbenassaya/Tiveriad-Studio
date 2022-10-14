using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Extensions;

public static class XTypeExtensions
{
    public static XProject GetProject(this XType type)
    {
        return type.Package?.GetProject() ?? throw new ArgumentNullException("XProject");
    }
    
    public static XModule GetModule(this XType type)
    {
        return type.Package.GetModule();
    }
    
    public static string GetPartialNamespace(this XType type)
    {
        return type.Package?.GetPartialNamespace() ?? string.Empty;
    }

}