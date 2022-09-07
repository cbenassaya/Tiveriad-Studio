using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("PropertyBase")]
public class XPropertyBase : XMemberBase
{
    public XPropertyBase()
    {
        Constraints = new List<XConstraint>();
    }

    [XmlArray("Constraints")]
    [XmlArrayItem("IsUnique", typeof(XIsUniqueConstraint))]
    [XmlArrayItem("Required", typeof(XRequiredConstraint))]
    [XmlArrayItem("MinLength", typeof(XMinLengthConstraint))]
    [XmlArrayItem("MaxLength", typeof(XMaxLengthConstraint))]
    public List<XConstraint> Constraints { get; set; }

    [XmlAttribute("Displayed")] public bool Displayed { get; set; }
}