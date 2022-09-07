using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("GreaterThanRule")]
public class XGreaterThanRule : XRule
{
    [XmlAttribute] public string Value { get; set; }
}