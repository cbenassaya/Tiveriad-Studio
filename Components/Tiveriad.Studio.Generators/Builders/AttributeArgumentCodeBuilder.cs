using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

public class AttributeArgumentCodeBuilder : ICodeBuilder
{
    private AttributeArgument _attributeArgument = new(Option.None<string>());

    public AttributeArgumentCodeBuilder WithValue(string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The attribute parameter value must be a valid, non-empty string.",
                nameof(value));

        _attributeArgument = _attributeArgument.With(Option.Some(value));
        return this;
    }

    internal AttributeArgument Build()
    {
        if (string.IsNullOrWhiteSpace(_attributeArgument.Value.ValueOrDefault()))
            throw new MissingBuilderSettingException(
                "Providing the name of the type parameter is required when building a type parameter.");

        return _attributeArgument;
    }
}