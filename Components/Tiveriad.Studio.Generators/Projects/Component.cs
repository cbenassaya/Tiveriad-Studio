using System.Xml.Serialization;

namespace Tiveriad.Studio.Generators.Projects;

[XmlRoot("Component", Namespace = "urn:project-template-mapping-2.0")]
public class Component : ElementBase
{
    [XmlArray("Dependencies")]
    [XmlArrayItem("Dependency", typeof(Dependency))]
    public List<Dependency> Dependencies { get; set; }


    [XmlArray("ComponentItems")]
    [XmlArrayItem("ComponentItem", typeof(ComponentItem))]
    public List<ComponentItem> ComponentItems { get; set; }


    [XmlAttribute("Type")] public string Type { get; set; }

    [XmlAttribute("Layer")] public string Layer { get; set; }
}