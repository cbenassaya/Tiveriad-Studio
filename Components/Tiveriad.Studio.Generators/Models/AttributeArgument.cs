namespace Tiveriad.Studio.Generators.Models;

public class AttributeArgument
{
    public AttributeArgument(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }


    public AttributeArgument Set(
        string value)
    {
        Value = value;
        return this;
    }

    public AttributeArgument With(string value)
    {
        return new AttributeArgument(value);
    }
}