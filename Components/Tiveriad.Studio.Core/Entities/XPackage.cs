using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Package")]
public class XPackage : XNamedElement
{
    [XmlIgnore] public XComponent Component { get; set; }
    [XmlIgnore] public XPackage Parent { get; set; }

    [XmlArray("Entities")]
    [XmlArrayItem("Entity", typeof(XEntity))]
    public List<XEntity> Entities { get; set; }

    [XmlArray("Enums")]
    [XmlArrayItem("Enum", typeof(XEnum))]
    public List<XEnum> Enums { get; set; }

    [XmlArray("Actions")]
    [XmlArrayItem("Action", typeof(XAction))]
    public List<XAction> Actions { get; set; }

    [XmlArray("EndPoints")]
    [XmlArrayItem("EndPoint", typeof(XEndPoint))]
    public List<XEndPoint> EndPoints { get; set; }

    [XmlArray("Contracts")]
    [XmlArrayItem("Contract", typeof(XContract))]
    public List<XContract> Contracts { get; set; }

    [XmlArray("Packages")]
    [XmlArrayItem("Package", typeof(XPackage))]
    public List<XPackage> Packages { get; set; }
}