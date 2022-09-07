using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Tiveriad.Studio.Core.Entities;

[Serializable]
[XmlRoot("Constraint")]
public abstract class XConstraint : XElementBase
{
    [XmlAttribute("Path")]
    [MaxLength(250)]
    public string Path { get; set; }

    [XmlIgnore] public XProperty ConstraintFor { get; set; }
    [XmlIgnore] public XClassifier Classifier { get; set; }
}