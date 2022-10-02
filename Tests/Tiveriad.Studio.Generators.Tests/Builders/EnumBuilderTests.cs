using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Sources;
using Xunit;

namespace Tiveriad.Studio.Generators.Tests.Builders;

public class EnumBuilderTests
{
    [Fact]
    public void Create_Enum_ToSourceCode_Works()
    {
        var builder = Code.CreateEnum();
        builder
            .WithAccessModifier(AccessModifier.Public)
            .WithName("Enum")
            .WithMembers(
                Code.CreateEnumMember("A"),
                Code.CreateEnumMember("B"),
                Code.CreateEnumMember("C")
            );

        var enumeration = builder.Build();

        Assert.Equal("public enum Enum {A, B, C}", enumeration.ToSourceCode());
    }
}