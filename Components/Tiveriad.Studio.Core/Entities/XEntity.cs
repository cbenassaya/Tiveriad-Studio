using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Tiveriad.Studio.Core.Attributes;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Entity")]
public class XEntity : XClassifier
{
    [XmlIgnore] private string _baseTypeReference;

    [XmlArray("RelationShips")]
    [XmlArrayItem("ManyToOne", typeof(XManyToOne))]
    [XmlArrayItem("OneToMany", typeof(XOneToMany))]
    [XmlArrayItem("ManyToMany", typeof(XManyToMany))]
    public List<XRelationShip> RelationShips { get; set; }

    [XmlElement] public XPersistence Persistence { get; set; }
    [XmlElement] public XAuditable Auditable { get; set; }
    [XmlElement] public XMultiTenancy MultiTenancy { get; set; }

    [XmlAttribute] public bool IsBusinessEntity { get; set; }

    [XmlIgnore]
    [BsonIgnore]
    [InjectWith("BaseTypeReference")]
    public XEntity BaseType { get; set; }

    [XmlAttribute("BaseType")]
    public string BaseTypeReference
    {
        get => string.IsNullOrEmpty(_baseTypeReference) ? BaseType?.ToString() ?? string.Empty : _baseTypeReference;
        set => _baseTypeReference = value;
    }
}