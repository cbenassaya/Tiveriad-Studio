using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlRoot("Rule")]
public class XRule : XElementBase
{
    [XmlIgnore] public XMemberBase RuleFor { get; set; }

    [XmlIgnore] public XAction Action { get; set; }

    [XmlAttribute("Path")]
    [MaxLength(250)]
    public string Path { get; set; }
}