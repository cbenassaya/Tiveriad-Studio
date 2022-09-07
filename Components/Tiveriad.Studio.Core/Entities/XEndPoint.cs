using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Tiveriad.Studio.Core.Attributes;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("EndPoint")]
public class XEndPoint : XService
{
    [XmlIgnore] private string _actionReference;

    [XmlAttribute("HttpMethod")] public XHttpMethod HttpMethod { get; set; }
    [XmlAttribute("Authorize")] public bool Authorize { get; set; }
    [XmlAttribute("Route")] public string Route { get; set; }

    [XmlAttribute("Action")]
    public string ActionReference
    {
        get => string.IsNullOrEmpty(_actionReference) ? Action?.ToString() ?? string.Empty : _actionReference;
        set => _actionReference = value;
    }

    [BsonIgnore]
    [XmlIgnore]
    [InjectWith("ActionReference")]
    public XAction Action { get; set; }
}