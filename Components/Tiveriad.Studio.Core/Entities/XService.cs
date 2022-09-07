using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Service")]
public class XService : XClassifier
{
    public XService()
    {
        Parameters = new List<XParameter>();
        Mappings = new List<XMapping>();
    }

    [XmlArray("Parameters")]
    [XmlArrayItem("Parameter", typeof(XParameter))]
    public List<XParameter> Parameters { get; set; }

    [XmlElement] public XResponse Response { get; set; }

    [XmlArray("Mappings")]
    [XmlArrayItem("Mapping", typeof(XMapping))]
    public List<XMapping> Mappings { get; set; }
}