using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XEntityExtensions
{
    public static ClassCodeBuilder ToBuilder(this XEntity entity)
    {
        var classBuilder = Code.CreateClass(entity.Name);

        if (entity.BaseType != null)
            classBuilder.WithInheritedClass(Code.CreateInternalType(entity.BaseType.Name, entity.BaseType.Namespace)
                .Build());

        classBuilder.WithImplementedInterface(
            Code
                .CreateInternalType(ComplexTypes.IENTITY)
                .WithGenericArgument(Code.CreateInternalType().WithName(entity.Name).WithNamespace(entity.Namespace)));

        if (entity.Persistence is { IsAuditable: true })
            classBuilder.WithImplementedInterface(
                Code
                    .CreateInternalType(ComplexTypes.IAUDITABLE)
                    .WithGenericArgument(
                        Code.CreateInternalType().WithName(entity.Name).WithNamespace(entity.Namespace)));

        classBuilder
            .WithNamespace(entity.Namespace)
            .WithProperties(
                entity.GetIds().Select(x => x.ToBuilder())
            )
            .WithProperties(
                entity.GetProperties().Select(x => x.ToBuilder())
            )
            .WithProperties(
                entity.GetManyToOneRelationShips().Select(x => x.ToBuilder())
            )
            .WithProperties(
                entity.GetManyToManyRelationShips().Select(x => x.ToBuilder())
            )
            .WithProperties(
                entity.GetOneToManyRelationShips().Select(x => x.ToBuilder())
            );
        return classBuilder;
    }

    public static ClassCodeBuilder ToPersistenceBuilder(this XEntity xEntity)
    {
        var classBuilder = Code.CreateClass($"{xEntity.Name}Configuration")
            .WithNamespace($"{xEntity.GetProject().RootNamespace}.Persistence.Configurations")
            .WithImplementedInterface(
                Code
                    .CreateInternalType(ComplexTypes.IENTITYTYPECONFIGURATION)
                    .WithGenericArgument(Code.CreateInternalType().WithName(xEntity.Name)
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
                                    .WithGenericArgument(Code.CreateInternalType().WithName(xEntity.Name)
                                        .WithNamespace(xEntity.Namespace)).Build()
                            )
                    )
                    .WithBody(GetPersistenceMethodBody(xEntity))
            );
        return classBuilder;
    }

    private static string GetPersistenceMethodBody(this XEntity xEntity)
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