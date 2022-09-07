using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Parameter")]
public class XParameter : XCompositeMemberBase
{
    [XmlAttribute("Source")]
    [MaxLength(50)]
    public string Source { get; set; }
}