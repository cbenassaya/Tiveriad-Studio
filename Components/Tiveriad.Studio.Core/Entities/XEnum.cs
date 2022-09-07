using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Enum")]
public class XEnum : XComplexType
{
    [XmlArray("Values")]
    [XmlArrayItem("Value", typeof(string))]
    public string[] Values { get; set; }
}