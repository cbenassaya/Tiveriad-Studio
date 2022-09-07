using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Net.SourceCode;
using Xunit;

namespace Tiveriad.Studio.Generators.Tests.Builders;

public class ClassBuilderTests
{
    [Fact]
    public void Create_Class_With_Properties_ToSourceCode_Works()
    {
        var expected = @"public class MyEntity : IEntity<string>, IAuditable<string>
{
    [MaxLength(24)]
    [Required]
    public int P1 { get; set; }

    public string P2 { get; set; }

    public bool P3 { get; set; }
}";
        var builder = Code.CreateClass();
        builder
            .WithAccessModifier(AccessModifier.Public)
            .WithName("MyEntity")
            .WithImplementedInterfaces(
                Code.CreateInternalType(NComplexTypes.IENTITY).WithGenericArgument(Code.CreateInternalType(NDataTypes.STRING)),
                Code.CreateInternalType(NComplexTypes.IAUDITABLE).WithGenericArgument(Code.CreateInternalType(NDataTypes.STRING))
                )
            .WithProperties(
                Code.CreateProperty(NDataTypes.INT, "P1")
                    .WithAttributes(
                        Code.CreateAttribute()
                            .WithType(NComplexTypes.MAXLENGTHATTRIBUTE)
                            .WithAttributeArgument(Code.CreateAttributeArgument().WithValue("24")),
                        Code.CreateAttribute()
                            .WithType(NComplexTypes.REQUIREDATTRIBUTE)),
                Code.CreateProperty(NDataTypes.STRING, "P2"),
                Code.CreateProperty(NDataTypes.BOOL, "P3")
                );

        var @class = builder.Build();
        
        Assert.Equal(expected, ((Class) @class).ToSourceCode().NormalizeWhitespace());
    }
    
    
    [Fact]
    public void Create_Class_With_Fields_ToSourceCode_Works()
    {
        var expected = @"public class MyEntity
{
    private int _p1;
    private string _p2;
    private bool _p3;
}";
        var builder = Code.CreateClass();
        builder
            .WithAccessModifier(AccessModifier.Public)
            .WithName("MyEntity")
            .WithFields(
                Code.CreateField(NDataTypes.INT, "_p1").WithAccessModifier(AccessModifier.Private),
                Code.CreateField(NDataTypes.STRING, "_p2").WithAccessModifier(AccessModifier.Private),
                Code.CreateField(NDataTypes.BOOL, "_p3").WithAccessModifier(AccessModifier.Private)
            );

        var @class = builder.Build();
        
        Assert.Equal(expected, ((Class) @class).ToSourceCode().NormalizeWhitespace());
    }
    
    
    [Fact]
    public void Create_Class_With_Constructors_ToSourceCode_Works()
    {
        var expected = @"public class MyEntity
{
    private int _p1;
    private string _p2;
    private bool _p3;
    public MyEntity(int p1, string p2, bool p3)
    {
        _p1 = p1;
        _p2 = p2;
        _p3 = p3;
    }

    public void MyMethod(string name, int age)
    {
    }
}";
        var builder = Code.CreateClass();
        builder
            .WithAccessModifier(AccessModifier.Public)
            .WithName("MyEntity")
            .WithFields(
                Code.CreateField(NDataTypes.INT, "_p1").WithAccessModifier(AccessModifier.Private),
                Code.CreateField(NDataTypes.STRING, "_p2").WithAccessModifier(AccessModifier.Private),
                Code.CreateField(NDataTypes.BOOL, "_p3").WithAccessModifier(AccessModifier.Private)
            )
            .WithMethod(
                Code
                    .CreateMethod()
                    .MakeConstructor(true)
                    .WithParameters(
                        Code.CreateParameter(NDataTypes.INT, "p1"),
                        Code.CreateParameter(NDataTypes.STRING, "p2"),
                        Code.CreateParameter(NDataTypes.BOOL, "p3")
                    )
                    .WithBody(@"_p1=p1;_p2=p2;_p3=p3;")
            )
            .WithMethod(Code.CreateMethod().WithName("MyMethod").WithParameters(
                Code.CreateParameter(NDataTypes.STRING, "name"),
                Code.CreateParameter(NDataTypes.INT, "age")));


        var @class = builder.Build();
        
        Assert.Equal(expected, ((Class) @class).ToSourceCode().NormalizeWhitespace());
    }
}