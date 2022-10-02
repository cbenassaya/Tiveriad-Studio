using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Net.InternalTypes;

public static class DataTypes
{
    public static InternalType STRING =>
        Code.CreateInternalType("string", "System", XDataTypes.STRING).Build();

    public static InternalType INT =>
        Code.CreateInternalType("int", "System", XDataTypes.INT).Build();

    public static InternalType BOOL =>
        Code.CreateInternalType("bool", "System", XDataTypes.BOOL).Build();

    public static InternalType DATETIME =>
        Code.CreateInternalType("DateTime", "System", XDataTypes.DATETIME).Build();


    public static List<InternalType> Types => new()
    {
        STRING, INT, BOOL, DATETIME
    };
}