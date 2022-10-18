using Tiveriad.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public class PersistenceBuilderRequestHandler : IRequestHandler<PersistenceBuilderRequest, ClassCodeBuilder>
{
    public Task<ClassCodeBuilder> Handle(PersistenceBuilderRequest request, CancellationToken cancellationToken)
    {
        var xEntity = request.Entity;
        var classBuilder = Code.CreateClass($"{xEntity.Name}Configuration")
            .WithNamespace($"{xEntity.GetProject().RootNamespace}.Persistence.Configurations")
            .WithReference(xEntity)
            .WithImplementedInterface(
                Code
                    .CreateInternalType(ComplexTypes.IENTITYTYPECONFIGURATION)
                    .WithGenericArgument(Code.CreateInternalType(xEntity)
                        .WithNamespace(xEntity.Namespace))
            )
            .WithMethod(
                Code
                    .CreateMethod()
                    .WithName("Configure")
                    .WithParameters(
                        Code
                            .CreateParameter()
                            .WithName("builder")
                            .WithType(
                                Code
                                    .CreateInternalType(ComplexTypes.ENTITYTYPEBUILDER)
                                    .WithGenericArgument(Code.CreateInternalType(xEntity)).Build()
                            )
                    )
                    .WithBody(GetPersistenceMethodBody(xEntity))
            );
        return Task.FromResult(classBuilder);
    }

    private static string GetPersistenceMethodBody(XEntity xEntity)
    {
        var children = xEntity.GetChildren();

        var builder = CodeBuilder.Instance();
        builder
            .AppendNewLine()
            .If(() => xEntity.BaseType == null).Append($"builder.ToTable(\"{xEntity.Persistence.Name}\")")
            .If(() => xEntity.BaseType == null && children.Any()).Append(".HasDiscriminator<string>(\"Discriminator\")")
            .Append(children, x => $".HasValue<{x.Name}>(\"{x.Name}\")", CodeBuilder.Separator.EmptySpace)
            .Append(";");


        var ids = xEntity.GetIds();
        var primaryKeyBuilder = CodeBuilder.Instance();

        if (xEntity.BaseType == null)
        {
            if (ids.Count > 1)
            {
                primaryKeyBuilder.Append("b=>");
                primaryKeyBuilder.Append("new { ");
                primaryKeyBuilder.Append(ids, id => $"b.{id.Name}", CodeBuilder.Separator.Comma);
                primaryKeyBuilder.Append("}");
            }

            if (ids.Count == 1) primaryKeyBuilder.Append($"b=>b.{ids.FirstOrDefault()?.Name}");

            builder
                .AppendNewLine("// <-- Id -->")
                .AppendNewLine()
                .If(() => ids.Any()).Append($"builder.HasKey({primaryKeyBuilder}).HasName(\"PK_{xEntity.Name}Id\");");
        }

        builder
            .AppendNewLine("// <-- ManyToOne -->")
            .AppendNewLine()
            .Append(xEntity.GetManyToOneRelationShips(), x => $"builder.HasOne(b => b.{x.Name});",
                CodeBuilder.Separator.NewLine);

        builder
            .AppendNewLine("// <-- OneToMany -->")
            .AppendNewLine()
            .Append(xEntity.GetOneToManyRelationShips(), x => GetOneToManyPersistencePropertyDeclaration(x),
                CodeBuilder.Separator.NewLine);

        builder
            .AppendNewLine("// <-- Enum -->")
            .AppendNewLine()
            .If<XProperty>(x => x.Type is XEnum)
            .Append(xEntity.GetProperties(),
                x =>
                    $"builder.Property(e => e.{x.Name}).HasConversion(v => v.ToString(), v => ({x.Type.Name})Enum.Parse(typeof({x.Type.Name}),v));",
                CodeBuilder.Separator.NewLine);
        builder
            .AppendNewLine("// <-- Object -->")
            .AppendNewLine()
            .If<XProperty>(x => x.Type is XComplexType and not XEnum)
            .Append(xEntity.GetProperties(),
                x =>
                    $"builder.Property(e => e.{x.Name}).HasConversion(v => v==null ? string.Empty : v.ToString(), v => string.IsNullOrEmpty(v) ? null: ({x.Type.Name})v);",
                CodeBuilder.Separator.NewLine);
        return builder.ToString();
    }

    private static string GetOneToManyPersistencePropertyDeclaration(XOneToMany xOneToMany)
    {
        var reverseRelationForManyToOne = xOneToMany.GetReverseRelation();
        var builder = CodeBuilder.Instance();
        builder
            .Append($"builder.HasMany(b => b.{xOneToMany.Name})")
            .If(() => reverseRelationForManyToOne != null)
            .Append($".WithOne(c => c.{reverseRelationForManyToOne?.Name})")
            .Append(";");
        return builder.ToString();
    }
}