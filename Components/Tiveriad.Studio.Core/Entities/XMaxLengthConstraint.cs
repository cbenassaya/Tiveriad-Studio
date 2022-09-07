using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("MaxLengthConstraint")]
public class XMaxLengthConstraint : XConstraint
{
    [XmlAttribute("Max")] public int Max { get; set; }
}