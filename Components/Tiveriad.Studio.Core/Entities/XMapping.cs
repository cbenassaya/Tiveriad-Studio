using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Tiveriad.Studio.Core.Attributes;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Mapping")]
public class XMapping : XElementBase
{
    private string _fromReference;
    private string _toReference;

    [XmlAttribute("From")]
    public string FromReference
    {
        get => string.IsNullOrEmpty(_fromReference) ? From?.ToString() ?? string.Empty : _fromReference;
        set => _fromReference = value;
    }

    [BsonIgnore]
    [XmlIgnore]
    [InjectWith("ActionReference")]
    public XType From { get; set; }


    [XmlAttribute("To")]
    public string ToReference
    {
        get => string.IsNullOrEmpty(_toReference) ? To?.ToString() ?? string.Empty : _toReference;
        set => _toReference = value;
    }

    [BsonIgnore]
    [XmlIgnore]
    [InjectWith("ToReference")]
    public XType To { get; set; }
}