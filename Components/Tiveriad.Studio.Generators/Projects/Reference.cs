using System.Xml.Serialization;

namespace Tiveriad.Studio.Generators.Projects;

[XmlRoot("Reference", Namespace = "urn:project-template-mapping-2.0")]
public class Reference
{
    [XmlAttribute("Name")] public string Name { get; set; }
}