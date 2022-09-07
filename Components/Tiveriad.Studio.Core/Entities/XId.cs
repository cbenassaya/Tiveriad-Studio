using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Id")]
public class XId : XPropertyBase
{
    public XId()
    {
        Name = "Id";
    }
}