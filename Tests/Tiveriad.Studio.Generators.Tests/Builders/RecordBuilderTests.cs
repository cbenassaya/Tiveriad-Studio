using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Net.SourceCode;
using Xunit;

namespace Tiveriad.Studio.Generators.Tests.Builders;

public class RecordBuilderTests
{

    [Fact]
    public void Create_Record_ToSourceCode_Works()
    {
        var builder = Code.CreateRecord("Record");
        builder
            .WithParameter(Code.CreateParameter(NDataTypes.STRING, "Name"))
            .WithParameter(Code.CreateParameter(NDataTypes.INT, "Age"))
            .WithImplementedInterface(
                Code
                    .CreateInternalType("G1", "N1")
                        .WithName("Interface")
                        .WithNamespace("Namespace")
                        .WithGenericArgument(Code.CreateInternalType("G1", "N1"))
                        .WithGenericArgument(Code.CreateInternalType("G2", "N2")));
        var @record = builder.Build();
        Assert.Equal("public record Record (string Name, int Age):Interface<G1,G2>;", @record.ToSourceCode());
    }
}