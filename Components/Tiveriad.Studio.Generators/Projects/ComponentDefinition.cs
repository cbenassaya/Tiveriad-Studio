using System.Xml.Serialization;

namespace Tiveriad.Studio.Generators.Projects;

[XmlRoot("ComponentDefinition", Namespace = "urn:project-template-mapping-2.0")]
public class ComponentDefinition : ElementBase
{
    [XmlArray("Dependencies")]
    [XmlArrayItem("Dependency", typeof(Dependency))]
    public List<Dependency> Dependencies { get; set; }


    [XmlArray("ComponentItems")]
    [XmlArrayItem("ComponentItem", typeof(ComponentItem))]
    public List<ComponentItem> ComponentItems { get; set; }

    [XmlArray("References")]
    [XmlArrayItem("Reference", typeof(Reference))]
    public List<Reference> References { get; set; }

    [XmlAttribute("Type")] public string Type { get; set; }

    [XmlAttribute("Layer")] public string Layer { get; set; }
    [XmlAttribute("NamePattern")] public string NamePattern { get; set; }
    [XmlAttribute("Template")] public string Template { get; set; }
}