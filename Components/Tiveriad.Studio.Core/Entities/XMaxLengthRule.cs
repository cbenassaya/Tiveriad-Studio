using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("MaxLengthRule")]
public class XMaxLengthRule : XRule
{
    [XmlAttribute] public int Max { get; set; }
}