using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.SourceCode;
using Xunit;

namespace Tiveriad.Studio.Generators.Tests.Builders;


public class InternalTypeBuilderTests
{
    [Fact]
    public void Create_InternalType_Works()
    {
        var internalType = Code.CreateInternalType("Type", "Namespace").Build();
        Assert.Equal("Type", internalType.Name.ValueOr(string.Empty));
        Assert.Equal("Namespace", internalType.Namespace.ValueOr(string.Empty));
    }
    
    [Fact]
    public void Create_InternalType_AndChangeNamespace_Works()
    {
        var builder = Code.CreateInternalType("Type", "FakeNamespace");
        var internalType = builder.WithNamespace("Namespace").Build();
        Assert.Equal("Type", internalType.Name.ValueOr(string.Empty));
        Assert.Equal("Namespace", internalType.Namespace.ValueOr(string.Empty));
    }
    
    [Fact]
    public void Create_InternalType_AndChangeName_Works()
    {
        var builder = Code.CreateInternalType("FakeType", "Namespace");
        var internalType = builder.WithName("Type").Build();
        Assert.Equal("Type", internalType.Name.ValueOr(string.Empty));
        Assert.Equal("Namespace", internalType.Namespace.ValueOr(string.Empty));
    }
    
    [Fact]
    public void Create_InternalType_WithFullBuilder_Works()
    {
        var builder = Code.CreateInternalType();
        var internalType = builder.WithName("Type").WithNamespace("Namespace").Build();
        Assert.Equal("Type", internalType.Name.ValueOr(string.Empty));
        Assert.Equal("Namespace", internalType.Namespace.ValueOr(string.Empty));
    }
    
    [Fact]
    public void Create_InternalType_WithGenericTypes_Works()
    {
        var builder = Code.CreateInternalType();
        var internalType = builder
            .WithName("Type")
            .WithNamespace("Namespace")
            .WithGenericArgument(Code.CreateInternalType("G1","N1"))
            .WithGenericArgument(Code.CreateInternalType("G2","N2"))
            .Build();
        Assert.Equal("Type", internalType.Name.ValueOr(string.Empty));
        Assert.Equal("Namespace", internalType.Namespace.ValueOr(string.Empty));
    }
    
    
    [Fact]
    public void Create_InternalType_ToSourceCode_Works()
    {
        var builder = Code.CreateInternalType();
        var internalType = builder
            .WithName("Type")
            .WithNamespace("Namespace")
            .WithGenericArgument(Code.CreateInternalType("G1","N1"))
            .WithGenericArgument(Code.CreateInternalType("G2","N2"))
            .Build();
        Assert.Equal("Type<G1,G2>", internalType.ToSourceCode());
    }
    
    [Fact]
    public void Create_InternalType_Not_ToSourceCodeExpected_Works()
    {
        var builder = Code.CreateInternalType();
        var internalType = builder
            .WithName("Type")
            .WithNamespace("Namespace")
            .WithGenericArgument(Code.CreateInternalType("G1","N1"))
            .WithGenericArgument(Code.CreateInternalType("G2","N2"))
            .WithGenericArgument(Code.CreateInternalType("G3","N3"))
            .Build();
        Assert.NotEqual("Type<G1,G2>", internalType.ToSourceCode());
    }
}