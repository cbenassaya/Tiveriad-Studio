using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XPropertyExtensions
{
    public static PropertyBuilder ToBuilder(this XProperty property)
    {
        InternalTypeBuilder typeBuilder;

        if (property.IsCollection)
        {
            Code.CreateInternalType(NComplexTypes.IENUMERABLE);
            typeBuilder = Code.CreateInternalType(NComplexTypes.IENUMERABLE);
            typeBuilder.WithGenericArgument(InternalTypeBuilderExtensions.ToBuilder(property.Type));
        }
        else
        {
            var nullable = property.Constraints.Any(x => x is XRequiredConstraint) ? "?" : string.Empty;
            typeBuilder = InternalTypeBuilderExtensions.ToBuilder(property.Type);
        }

        var builder = Code.CreateProperty().WithType(typeBuilder.Build()).WithName(property.Name);
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMaxLengthConstraint)
                .Cast<XMaxLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMinLengthConstraint)
                .Cast<XMinLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        return builder;
    }

    public static PropertyBuilder ToBuilder(this XId property)
    {
        var builder = Code.CreateProperty().WithType(InternalTypeBuilderExtensions.ToBuilder(property.Type).Build()).WithName(property.Name);
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMaxLengthConstraint)
                .Cast<XMaxLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMinLengthConstraint)
                .Cast<XMinLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        return builder;
    }

    public static PropertyBuilder ToBuilder(this XManyToOne property)
    {
        var builder = Code.CreateProperty()
            .WithType(Code.CreateInternalType(property.Type.Name, property.Type.Namespace).Build())
            .WithName(property.Name);
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMaxLengthConstraint)
                .Cast<XMaxLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMinLengthConstraint)
                .Cast<XMinLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());

        return builder;
    }

    public static PropertyBuilder ToBuilder(this XOneToMany property)
    {
        var typeBuilder = Code.CreateInternalType(NComplexTypes.IENUMERABLE);
        typeBuilder.WithGenericArgument(Code.CreateInternalType(property.Type.Name, property.Type.Namespace));
        var builder = Code.CreateProperty().WithType(typeBuilder.Build()).WithName(property.Name);
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMaxLengthConstraint)
                .Cast<XMaxLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMinLengthConstraint)
                .Cast<XMinLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        return builder;
    }

    public static PropertyBuilder ToBuilder(this XManyToMany property)
    {
        var typeBuilder = Code.CreateInternalType(NComplexTypes.IENUMERABLE);
        typeBuilder.WithGenericArgument(Code.CreateInternalType(property.Type.Name, property.Type.Namespace));
        var builder = Code.CreateProperty().WithType(typeBuilder.Build()).WithName(property.Name);
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMaxLengthConstraint)
                .Cast<XMaxLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XMinLengthConstraint)
                .Cast<XMinLengthConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        builder.WithAttributes(
            property.Constraints
                .Where(x => x is XIsUniqueConstraint)
                .Cast<XIsUniqueConstraint>()
                .Select(x => x.ToBuilder())
                .ToArray());
        return builder;
    }
}