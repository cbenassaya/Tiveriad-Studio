using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XPropertyExtensions
{
    public static PropertyCodeBuilder ToBuilder(this XProperty property)
    {
        InternalTypeCodeBuilder typeCodeBuilder;

        if (property.IsCollection)
        {
            Code.CreateInternalType(ComplexTypes.IENUMERABLE);
            typeCodeBuilder = Code.CreateInternalType(ComplexTypes.IENUMERABLE);
            typeCodeBuilder.WithGenericArgument(property.Type.ToBuilder());
        }
        else
        {
            var nullable = property.Constraints.Any(x => x is XRequiredConstraint) ? "?" : string.Empty;
            typeCodeBuilder = property.Type.ToBuilder();
        }

        var builder = Code.CreateProperty().WithType(typeCodeBuilder.Build()).WithName(property.Name);
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

    public static PropertyCodeBuilder ToBuilder(this XId property)
    {
        var builder = Code.CreateProperty().WithType(property.Type.ToBuilder().Build()).WithName(property.Name);
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

    public static PropertyCodeBuilder ToBuilder(this XManyToOne property)
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

    public static PropertyCodeBuilder ToBuilder(this XOneToMany property)
    {
        var typeBuilder = Code.CreateInternalType(ComplexTypes.IENUMERABLE);
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

    public static PropertyCodeBuilder ToBuilder(this XManyToMany property)
    {
        var typeBuilder = Code.CreateInternalType(ComplexTypes.IENUMERABLE);
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