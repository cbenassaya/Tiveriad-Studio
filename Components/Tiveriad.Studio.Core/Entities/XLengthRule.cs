using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("LengthRule")]
public class XLengthRule : XRule
{
    [XmlAttribute] public int Min { get; set; }
    [XmlAttribute] public int Max { get; set; }
}