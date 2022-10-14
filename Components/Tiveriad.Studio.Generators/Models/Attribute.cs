using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Attribute
{
    public Attribute(
        InternalType internalType,
        List<AttributeArgument> attributeArgument)
    {
        InternalType = internalType;
        AttributeArguments = attributeArgument ?? new List<AttributeArgument>();
    }

    public InternalType InternalType { get; private set; }
    public List<AttributeArgument> AttributeArguments { get; }

    public Attribute Set(
        InternalType name)
    {
        InternalType = name;
        return this;
    }

    public static Attribute With(InternalType internalType)
    {
        return new Attribute(internalType, new List<AttributeArgument>());
    }
}