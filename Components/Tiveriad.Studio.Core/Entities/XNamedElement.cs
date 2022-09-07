using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("NamedElement")]
public class XNamedElement : XElementBase
{
    [XmlAttribute("Name")] [MaxLength(50)] public string Name { get; set; }

    [XmlAttribute("PluralName")]
    [MaxLength(50)]
    public string PluralName { get; set; }
}