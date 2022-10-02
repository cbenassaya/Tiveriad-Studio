using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Property")]
public class XProperty : XPropertyBase
{
    [XmlAttribute("BusinessKey")] public bool BusinessKey { get; set; }

    [XmlAttribute("IsCollection")] public bool IsCollection { get; set; }
}