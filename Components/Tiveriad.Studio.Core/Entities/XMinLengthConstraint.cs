using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("MinLengthConstraint")]
public class XMinLengthConstraint : XConstraint
{
    [XmlAttribute("Min")] public int Min { get; set; }
}