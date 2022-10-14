using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlRoot("Project", Namespace = "urn:xcore-mapping-2.0")]
public class XProject : XNamedElement
{

    [XmlElement("Module")] public List<XModule> Modules { get; set; }

    [XmlAttribute("RootNamespace")]
    [MaxLength(200)]
    public string RootNamespace { get; set; }

    [XmlArray("Metadata")]
    [XmlArrayItem("KeyValue", typeof(XKeyValue))]
    public List<XKeyValue> Metadata { get; set; }
    
    [XmlIgnore] public Guid ReferenceId { get;  } = Guid.NewGuid();
}