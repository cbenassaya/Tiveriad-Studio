using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Parameter
{
    public Parameter(
        Option<InternalType> type = default,
        Option<string> name = default,
        Option<string> receivingMember = default,
        Option<List<Attribute>> attributes = default)
    {
        Type = type;
        Name = name;
        ReceivingMember = receivingMember;
        Attributes = attributes.ValueOr(new List<Attribute>());
    }

    public Option<InternalType> Type { get; private set; }

    public Option<string> Name { get; private set; }
    
    public List<Attribute> Attributes { get; private set; }

    public Option<string> ReceivingMember { get; private set; }

    public Parameter Set(
        Option<InternalType> type = default,
        Option<string> name = default,
        Option<List<Attribute>> attributes = default,
        Option<string> receivingMember = default)
    {
        Name = name.Else(Name);
        Type = type.Else(Type);
        Attributes = attributes.ValueOr(Attributes);
        ReceivingMember = receivingMember.Else(ReceivingMember);
        return this;
    }
}