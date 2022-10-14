namespace Tiveriad.Studio.Generators.Models;

public class Parameter
{
    public Parameter(
        InternalType? type = default,
        string? name = default,
        string? receivingMember = default,
        List<Attribute>? attributes = default)
    {
        Type = type;
        Name = name ?? string.Empty;
        ReceivingMember = receivingMember ?? string.Empty;
        Attributes = attributes ?? new List<Attribute>();
    }

    public InternalType? Type { get; private set; }

    public string Name { get; private set; }

    public List<Attribute> Attributes { get; private set; }

    public string ReceivingMember { get; private set; }

    public Parameter Set(
        InternalType? type = default,
        string? name = default,
        List<Attribute>? attributes = default,
        string? receivingMember = default)
    {
        Name = name ?? Name;
        Type = type ?? Type;
        Attributes = attributes ?? Attributes;
        ReceivingMember = receivingMember ?? ReceivingMember;
        return this;
    }
}