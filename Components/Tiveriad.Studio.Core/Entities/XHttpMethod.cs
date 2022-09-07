using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("HttpMethod")]
public enum XHttpMethod
{
    [XmlEnum(Name = "HttpPost")] HttpPost,
    [XmlEnum(Name = "HttpGet")] HttpGet,
    [XmlEnum(Name = "HttpPut")] HttpPut,
    [XmlEnum(Name = "HttpDelete")] HttpDelete
}