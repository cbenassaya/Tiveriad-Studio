using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("EqualRule")]
public class XEqualRule : XRule
{
    [XmlAttribute] public string Value { get; set; }
    [XmlAttribute] public bool IsExpression { get; set; }
}