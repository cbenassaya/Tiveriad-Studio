using System.Xml.Serialization;

namespace Tiveriad.Studio.Generators.Projects;

[XmlRoot("Dependency", Namespace = "urn:project-template-mapping-2.0")]
public class Dependency
{
    [XmlAttribute("Include")] public string Include { get; set; }

    [XmlAttribute("Version")] public string Version { get; set; }
}