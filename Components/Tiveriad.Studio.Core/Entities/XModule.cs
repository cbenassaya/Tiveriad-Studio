using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Module")]
public class XModule : XNamedElement
{
    public XModule()
    {
        Packages = new List<XPackage>();
    }

    [XmlIgnore] public XProject Project { get; set; }
    [XmlElement("Package")] public List<XPackage> Packages { get; set; }

    [XmlIgnore] public Guid ReferenceId { get; } = Guid.NewGuid();
}