using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("CompositeMemberBase")]
public class XCompositeMemberBase : XMemberBase
{
    [XmlAttribute("IsCollection")] public bool IsCollection { get; set; }
}