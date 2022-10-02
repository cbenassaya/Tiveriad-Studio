using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Net.Sources;
using Xunit;

namespace Tiveriad.Studio.Generators.Tests.Builders;

public class ClassBuilderTests
{
    [Fact]
    public void Create_Class_With_Properties_ToSourceCode_Works()
    {
        var expected = @"namespace NS;
public class MyEntity : IEntity<string>, IAuditable<string>
{
    [MaxLength(24)]
    [Required]
    public int P1 { get; set; }

    public string P2 { get; set; }

    public bool P3 { get; set; }
}";
        var builder = Code.CreateClass();
        builder
            .WithNamespace("NS")
            .WithAccessModifier(AccessModifier.Public)
            .WithNamespace("NS")
            .WithName("MyEntity")
            .WithImplementedInterfaces(
                Code.CreateInternalType(ComplexTypes.IENTITY)
                    .WithGenericArgument(Code.CreateInternalType(DataTypes.STRING)),
                Code.CreateInternalType(ComplexTypes.IAUDITABLE)
                    .WithGenericArgument(Code.CreateInternalType(DataTypes.STRING))
            )
            .WithProperties(
                Code.CreateProperty(DataTypes.INT, "P1")
                    .WithAttributes(
                        Code.CreateAttribute()
                            .WithType(ComplexTypes.MAXLENGTHATTRIBUTE)
                            .WithAttributeArgument(Code.CreateAttributeArgument().WithValue("24")),
                        Code.CreateAttribute()
                            .WithType(ComplexTypes.REQUIREDATTRIBUTE)),
                Code.CreateProperty(DataTypes.STRING, "P2"),
                Code.CreateProperty(DataTypes.BOOL, "P3")
            );

        var @class = builder.Build();

        Assert.Equal(expected, @class.ToSourceCode().NormalizeWhitespace());
    }


    [Fact]
    public void Create_Class_With_Fields_ToSourceCode_Works()
    {
        var expected = @"namespace NS;
public class MyEntity
{
    private int _p1;
    private string _p2;
    private bool _p3;
}";
        var builder = Code.CreateClass();
        builder
            .WithNamespace("NS")
            .WithAccessModifier(AccessModifier.Public)
            .WithName("MyEntity")
            .WithFields(
                Code.CreateField(DataTypes.INT, "_p1").WithAccessModifier(AccessModifier.Private),
                Code.CreateField(DataTypes.STRING, "_p2").WithAccessModifier(AccessModifier.Private),
                Code.CreateField(DataTypes.BOOL, "_p3").WithAccessModifier(AccessModifier.Private)
            );

        var @class = builder.Build();

        Assert.Equal(expected, @class.ToSourceCode().NormalizeWhitespace());
    }


    [Fact]
    public void Create_Class_With_Constructors_ToSourceCode_Works()
    {
        var expected = @"namespace NS;
public class MyEntity
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
            .WithNamespace("NS")
            .WithAccessModifier(AccessModifier.Public)
            .WithName("MyEntity")
            .WithFields(
                Code.CreateField(DataTypes.INT, "_p1").WithAccessModifier(AccessModifier.Private),
                Code.CreateField(DataTypes.STRING, "_p2").WithAccessModifier(AccessModifier.Private),
                Code.CreateField(DataTypes.BOOL, "_p3").WithAccessModifier(AccessModifier.Private)
            )
            .WithMethod(
                Code
                    .CreateMethod()
                    .MakeConstructor(true)
                    .WithParameters(
                        Code.CreateParameter(DataTypes.INT, "p1"),
                        Code.CreateParameter(DataTypes.STRING, "p2"),
                        Code.CreateParameter(DataTypes.BOOL, "p3")
                    )
                    .WithBody(@"_p1=p1;_p2=p2;_p3=p3;")
            )
            .WithMethod(Code.CreateMethod().WithName("MyMethod").WithParameters(
                Code.CreateParameter(DataTypes.STRING, "name"),
                Code.CreateParameter(DataTypes.INT, "age")));


        var @class = builder.Build();

        Assert.Equal(expected, @class.ToSourceCode().NormalizeWhitespace());
    }
}