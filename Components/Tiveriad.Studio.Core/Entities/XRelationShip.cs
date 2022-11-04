using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("RelationShip")]
public abstract class XRelationShip : XMemberBase
{
    [XmlArray("Constraints")]
    [XmlArrayItem("IsUnique", typeof(XIsUniqueConstraint))]
    [XmlArrayItem("Required", typeof(RequiredConstraint))]
    [XmlArrayItem("MinLength", typeof(XMinLengthConstraint))]
    [XmlArrayItem("MaxLength", typeof(XMaxLengthConstraint))]
    public List<XConstraint> Constraints { get; set; }
}