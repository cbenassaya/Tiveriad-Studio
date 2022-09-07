using System.Collections.Generic;

namespace Tiveriad.Studio.Core.Entities;

public static class XDataTypes
{
    public static XDataType STRING => new()
    {
        Name = "string", Namespace = "System"
    };

    public static XDataType INT => new()
    {
        Name = "int", Namespace = "System"
    };

    public static XDataType DOUBLE => new()
    {
        Name = "double", Namespace = "System"
    };

    public static XDataType BOOL => new()
    {
        Name = "bool", Namespace = "System"
    };

    public static XDataType DATETIME => new()
    {
        Name = "DateTime", Namespace = "System"
    };


    public static List<XDataType> Types => new()
    {
        STRING, INT, BOOL, DATETIME, DOUBLE
    };
}