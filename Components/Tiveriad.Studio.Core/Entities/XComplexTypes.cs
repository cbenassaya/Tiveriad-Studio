using System.Collections.Generic;

namespace Tiveriad.Studio.Core.Entities;

public static class XComplexTypes
{
    public static XComplexType METADATA => new()
    {
        Name = "Metadata", Namespace = "Tiveriad.Commons.Types"
    };


    public static List<XComplexType> Types => new()
    {
        METADATA
    };
}