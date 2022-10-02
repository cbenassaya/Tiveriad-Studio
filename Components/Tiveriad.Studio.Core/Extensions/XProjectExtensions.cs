using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Processors;

namespace Tiveriad.Studio.Core.Extensions;

public static class XProjectExtensions
{
    public static IList<T> GetChildren<T>(this XProject project)
    {
        var processor = new FindByTypeProcessor<T>();
        processor.Traverse(project);
        return processor.Values;
    }

    public static List<XKeyValue> GetMetadata(this XProject xProject)
    {
        return xProject.Metadata ?? new List<XKeyValue>();
    }

    public static void Add(this XProject xProject, XKeyValue xKeyValue)
    {
        if (xProject.Metadata == null)
            xProject.Metadata = new List<XKeyValue>();

        xProject.Metadata.Add(xKeyValue);
    }
}