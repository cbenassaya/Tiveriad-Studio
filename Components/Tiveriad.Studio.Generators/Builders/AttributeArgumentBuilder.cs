using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

public class AttributeArgumentBuilder : AbstractBuilder
{
    internal AttributeArgumentBuilder()
    {
    }

    internal AttributeArgument AttributeArgument { get; private set; } = new AttributeArgument(value: Option.None<string>());
    
    public AttributeArgumentBuilder WithValue(string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The attribute parameter value must be a valid, non-empty string.", nameof(value));

        AttributeArgument = AttributeArgument.With(value: Option.Some(value));
        return this;
    }
    
    internal AttributeArgument Build()
    {
        if (string.IsNullOrWhiteSpace(AttributeArgument.Value.ValueOrDefault()))
        {
            throw new MissingBuilderSettingException(
                "Providing the name of the type parameter is required when building a type parameter.");
        }

        return AttributeArgument;
    }
    
}