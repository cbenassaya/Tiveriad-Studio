using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("LessThanRule")]
public class XLessThanRule : XRule
{
    [XmlAttribute] public string Value { get; set; }
}