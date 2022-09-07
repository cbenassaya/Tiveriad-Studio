using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("LessThanOrEqualRule")]
public class XLessThanOrEqualRule : XRule
{
    [XmlAttribute] public double Value { get; set; }
}