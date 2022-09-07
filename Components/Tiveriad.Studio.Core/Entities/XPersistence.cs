using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Tiveriad.Studio.Core.Attributes;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Persistence")]
public class XPersistence : XNamedElement
{
    [XmlIgnore] private string _auditableKeyTypeReference;
    [XmlIgnore] public XEntity Entity { get; set; }

    [XmlAttribute("IsAuditable")] public bool IsAuditable { get; set; }

    [InjectWith("AuditableKeyTypeReference")]
    [XmlIgnore]
    [BsonIgnore]
    public XType AuditableKeyType { get; set; }

    [XmlAttribute("AuditableKeyType")]
    public string AuditableKeyTypeReference
    {
        get => string.IsNullOrEmpty(_auditableKeyTypeReference)
            ? AuditableKeyType?.ToString() ?? string.Empty
            : _auditableKeyTypeReference;
        set => _auditableKeyTypeReference = value;
    }
}