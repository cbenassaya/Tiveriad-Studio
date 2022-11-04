using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Tiveriad.Studio.Core.Attributes;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("MultiTenancy")]
public class XMultiTenancy : XElementBase
{
    [XmlIgnore] private string _keyType;
    [XmlIgnore] public XEntity Entity { get; set; }
    

    [InjectWith("KeyTypeReference")]
    [XmlIgnore]
    [BsonIgnore]
    public XType KeyType { get; set; }

    [XmlAttribute("KeyType")]
    public string KeyTypeReference
    {
        get => string.IsNullOrEmpty(_keyType)
            ? KeyType?.ToString() ?? string.Empty
            : _keyType;
        set => _keyType = value;
    }
}