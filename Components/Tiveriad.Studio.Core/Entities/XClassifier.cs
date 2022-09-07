using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Classifier")]
public class XClassifier : XComplexType
{
    public XClassifier()
    {
        Properties = new List<XPropertyBase>();
    }


    [XmlArray("Properties")]
    [XmlArrayItem("Id", typeof(XId))]
    [XmlArrayItem("Property", typeof(XProperty))]
    public List<XPropertyBase> Properties { get; set; }
}