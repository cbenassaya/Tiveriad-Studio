using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("RegularExpressionRule")]
public class XRegularExpressionRule : XRule
{
    [XmlAttribute] public string Expression { get; set; }
}