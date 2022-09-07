using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Tiveriad.Studio.Core.Attributes;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("MemberBase")]
public class XMemberBase : XNamedElement
{
    [XmlIgnore] private string _typeReference;

    [InjectWith("TypeReference")]
    [XmlIgnore]
    [BsonIgnore]
    public XType Type { get; set; }


    [XmlIgnore] public XClassifier Classifier { get; set; }


    [XmlAttribute("Type")]
    public string TypeReference
    {
        get => string.IsNullOrEmpty(_typeReference) ? Type?.ToString() ?? string.Empty : _typeReference;
        set => _typeReference = value;
    }
}