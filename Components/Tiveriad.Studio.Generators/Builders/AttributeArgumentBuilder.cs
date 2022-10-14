using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

public class AttributeArgumentBuilder : ICodeBuilder
{
    private AttributeArgument _attributeArgument = new(string.Empty);

    public AttributeArgumentBuilder WithValue(string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The attribute parameter value must be a valid, non-empty string.",
                nameof(value));

        _attributeArgument = _attributeArgument.With(value);
        return this;
    }

    internal AttributeArgument Build()
    {
        if (string.IsNullOrWhiteSpace(_attributeArgument.Value))
            throw new MissingBuilderSettingException(
                "Providing the name of the type parameter is required when building a type parameter.");

        return _attributeArgument;
    }
}