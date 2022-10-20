using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Type")]
public abstract class XType : XNamedElement
{
    [XmlIgnore] public XPackage Package { get; set; }

    [XmlAttribute("Namespace")]
    [MaxLength(200)]
    public string Namespace { get; set; }

    public override string ToString()
    {
        return $"{Namespace}.{Name}";
    }

    protected bool Equals(XType other)
    {
        return Namespace == other.Namespace && Name == other.Name;
    }

    public override bool Equals(object obj)
    {
        return ReferenceEquals(this, obj) || obj is XType other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name ?? string.Empty, Namespace ?? string.Empty);
    }
}