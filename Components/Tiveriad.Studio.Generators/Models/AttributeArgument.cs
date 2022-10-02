using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class AttributeArgument
{
    public AttributeArgument(Option<string> value = default)
    {
        Value = value;
    }

    public Option<string> Value { get; private set; }


    public AttributeArgument Set(
        Option<string> value = default)
    {
        Value = value.Else(Value);
        return this;
    }

    public AttributeArgument With(Option<string> value)
    {
        return new(
            value.Else(Value));
    }
}