using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("NotEqualRule")]
public class XNotEqualRule : XRule
{
    [XmlAttribute] public string Value { get; set; }
    [XmlAttribute] public bool IsExpression { get; set; }
}