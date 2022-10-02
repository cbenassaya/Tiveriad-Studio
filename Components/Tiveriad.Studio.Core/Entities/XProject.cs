using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlRoot("Project", Namespace = "urn:xcore-mapping-2.0")]
public class XProject : XNamedElement
{
    [XmlElement("Component")] public List<XComponent> Components { get; set; }

    [XmlAttribute("RootNamespace")]
    [MaxLength(200)]
    public string RootNamespace { get; set; }

    [XmlArray("Metadata")]
    [XmlArrayItem("KeyValue", typeof(XKeyValue))]
    public List<XKeyValue> Metadata { get; set; }
}