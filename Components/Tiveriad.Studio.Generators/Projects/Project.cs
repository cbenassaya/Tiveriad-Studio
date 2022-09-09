using System.Xml.Serialization;

namespace Tiveriad.Studio.Generators.Projects;


[XmlRoot("ProjectTemplate", Namespace = "urn:project-template-mapping-2.0")]
public class ProjectTemplate
{
    [XmlArray("Components")]
    [XmlArrayItem("ComponentType", typeof(ComponentType))]
    public List<ComponentType> Components { get; set; }
}

public class ElementBase { }

[XmlRoot("ComponentType", Namespace = "urn:project-template-mapping-2.0")]
public class ComponentType: ElementBase
{
    
    
    [XmlArray("Dependencies")]
    [XmlArrayItem("Dependency", typeof(Dependency))]
    public List<Dependency> Dependencies { get; set; }
    
    
    [XmlArray("ComponentItems")]
    [XmlArrayItem("ComponentItem", typeof(ComponentItem))]
    public List<ComponentItem> ComponentItems { get; set; }
    

    
    [XmlAttribute("Type")]
    public string Type { get; set; }
}

[XmlRoot("Dependency", Namespace = "urn:project-template-mapping-2.0")]
public class Dependency
{
    [XmlAttribute("Include")]
    public string Include { get; set; }
    [XmlAttribute("Version")]
    public string Version { get; set; }
}
[XmlRoot("ComponentItem", Namespace = "urn:project-template-mapping-2.0")]
public class ComponentItem 
{
    [XmlAttribute("Stereotype")]
    public string Stereotype { get; set; }
    [XmlAttribute("Pattern")]
    public string Pattern { get; set; }
}