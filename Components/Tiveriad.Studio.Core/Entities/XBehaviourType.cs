using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("BehaviourType")]
public enum XBehaviourType
{
    [XmlEnum(Name = "Command")] Command,
    [XmlEnum(Name = "GetOne")] GetOne,
    [XmlEnum(Name = "GetMany")] GetMany,
    [XmlEnum(Name = "Query")] Query,
    [XmlEnum(Name = "SaveOrUpdate")] SaveOrUpdate,
    [XmlEnum(Name = "Delete")] Delete
}