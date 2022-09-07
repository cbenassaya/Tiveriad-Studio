using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Attribute
{

    public Attribute(
        Option<InternalType> internalType = default,
        Option<List<AttributeArgument>> attributeArgument = default)
    {
        InternalType = internalType;
        AttributeArguments = attributeArgument.ValueOr(new List<AttributeArgument>());
    }
        
    public Option<InternalType> InternalType { get;  private set; }
    public List<AttributeArgument> AttributeArguments { get;  private set; }
        
    public  Attribute Set(
        Option<InternalType> name = default)
    {
        InternalType = name.Else(InternalType);
        return this;
    }
    public static Attribute With(
        Option<InternalType> internalType = default) =>
        new Attribute( internalType );
}