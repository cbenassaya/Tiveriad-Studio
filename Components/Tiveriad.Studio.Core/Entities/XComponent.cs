using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Component")]
public class XComponent : XNamedElement
{
    public XComponent()
    {
        Packages = new List<XPackage>();
    }

    [XmlIgnore] public XProject Project { get; set; }
    [XmlElement("Package")] public List<XPackage> Packages { get; set; }
}