using System.Xml.Serialization;

namespace Tiveriad.Studio.Generators.Projects;


[XmlRoot("ProjectTemplate", Namespace = "urn:project-template-mapping-2.0")]
public class ProjectTemplate
{
    [XmlArray("Components")]
    [XmlArrayItem("Component", typeof(Component))]
    public List<Component> Components { get; set; }
}