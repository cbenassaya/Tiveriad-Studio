using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("KeyValue")]
public class XKeyValue : XElementBase
{
    [XmlAttribute("Key")] public string Key { get; set; }
    [XmlAttribute("Value")] public string Value { get; set; }
}