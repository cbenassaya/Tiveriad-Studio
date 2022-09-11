using System.Xml.Serialization;

namespace Tiveriad.Studio.Generators.Projects;

[XmlRoot("ComponentItem", Namespace = "urn:project-template-mapping-2.0")]
public class ComponentItem 
{
    [XmlAttribute("Stereotype")]
    public string Stereotype { get; set; }
    [XmlAttribute("Pattern")]
    public string Pattern { get; set; }
}