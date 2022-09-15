using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Net.Sources;
using Xunit;

namespace Tiveriad.Studio.Generators.Tests.Builders;

public class PropertyBuilderTests
{

    [Fact]
    public void Create_Property_ToSourceCode_Works()
    {
        var builder = Code.CreateProperty();
        builder
            .WithType(DataTypes.INT)
            .WithName("Property");
        var @record = builder.Build();
        Assert.Equal("public int Property { get; set; }", @record.ToSourceCode());
    }

}