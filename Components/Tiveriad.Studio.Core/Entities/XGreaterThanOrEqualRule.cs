using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("GreaterThanOrEqualRule")]
public class XGreaterThanOrEqualRule : XRule
{
    [XmlAttribute] public string Value { get; set; }
}