using System.Xml.Serialization;

namespace Tiveriad.Studio.Generators.Projects;

[XmlRoot("ProjectDefinitionTemplate", Namespace = "urn:project-template-mapping-2.0")]
public class ProjectDefinitionTemplate
{
    [XmlArray("ComponentDefinitions")]
    [XmlArrayItem("ComponentDefinition", typeof(ComponentDefinition))]
    public List<ComponentDefinition> ComponentDefinitions { get; set; }
}