using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("PredicateRule")]
public class XPredicateRule : XRule
{
    [XmlAttribute] public string PredicateName { get; set; }
}