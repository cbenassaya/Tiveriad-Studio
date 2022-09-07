using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("MinLengthRule")]
public class XMinLengthRule : XRule
{
    [XmlAttribute] public int Min { get; set; }
}