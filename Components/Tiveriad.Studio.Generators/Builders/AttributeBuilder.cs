using Tiveriad.Studio.Generators.Models;
using Attribute = Tiveriad.Studio.Generators.Models.Attribute;

namespace Tiveriad.Studio.Generators.Builders;

public class AttributeBuilder
{
    private readonly List<AttributeArgumentBuilder> _attributeArguments = new();
    private Attribute _attribute;

    public AttributeBuilder WithType(InternalType internalType)
    {
        if (internalType is null)
            throw new ArgumentNullException(nameof(internalType));

        _attribute = Attribute.With(internalType);
        return this;
    }

    /// <summary>
    ///     Adds a type parameter to the field being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builder" /> is <c>null</c>.
    /// </exception>
    public AttributeBuilder WithAttributeArgument(AttributeArgumentBuilder builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        _attributeArguments.Add(builder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the field being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public AttributeBuilder WithAttributeArguments(params AttributeArgumentBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _attributeArguments.AddRange(builders);
        return this;
    }


    internal Attribute Build()
    {
        if (_attribute?.InternalType == null)
            throw new MissingBuilderSettingException(
                "Providing the internalType is required when building an attribute.");

        _attribute.AttributeArguments.AddRange(_attributeArguments.Select(builder => builder.Build()));

        return _attribute;
    }
}