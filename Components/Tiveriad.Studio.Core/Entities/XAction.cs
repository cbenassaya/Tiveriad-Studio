using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Tiveriad.Studio.Core.Attributes;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Action")]
public class XAction : XService
{
    [XmlIgnore] private string _entityReference;


    [XmlAttribute("Entity")]
    public string EntityReference
    {
        get => string.IsNullOrEmpty(_entityReference) ? Entity?.ToString() ?? string.Empty : _entityReference;
        set => _entityReference = value;
    }

    [BsonIgnore]
    [XmlIgnore]
    [InjectWith("EntityReference")]
    public XEntity Entity { get; set; }

    [XmlAttribute("BehaviourType")] public XBehaviourType BehaviourType { get; set; }

    [XmlArray("PreConditions")]
    [XmlArrayItem("MinLength", typeof(XMinLengthRule))]
    [XmlArrayItem("MaxLength", typeof(XMaxLengthRule))]
    [XmlArrayItem("RegularExpression", typeof(XRegularExpressionRule))]
    [XmlArrayItem("Predicate", typeof(XPredicateRule))]
    [XmlArrayItem("LessThan", typeof(XLessThanRule))]
    [XmlArrayItem("GreaterThan", typeof(XGreaterThanRule))]
    [XmlArrayItem("GreaterThanOrEqual", typeof(XGreaterThanOrEqualRule))]
    [XmlArrayItem("LessThanOrEqual", typeof(XLessThanOrEqualRule))]
    [XmlArrayItem("Equal", typeof(XEqualRule))]
    [XmlArrayItem("NotEqual", typeof(XNotEqualRule))]
    [XmlArrayItem("Length", typeof(XLengthRule))]
    [XmlArrayItem("NotEmpty", typeof(XNotEmptyRule))]
    [XmlArrayItem("NotNull", typeof(XNotNullRule))]
    public List<XRule> PreConditions { get; set; }

    [XmlArray("Services")]
    [XmlArrayItem("Service", typeof(string))]
    public List<string> ServiceReferences { get; set; }

    [XmlIgnore] public List<XService> Service { get; set; }

    [XmlArray("PostConditions")]
    [XmlArrayItem("MinLength", typeof(XMinLengthRule))]
    [XmlArrayItem("MaxLength", typeof(XMaxLengthRule))]
    [XmlArrayItem("RegularExpression", typeof(XRegularExpressionRule))]
    [XmlArrayItem("Predicate", typeof(XPredicateRule))]
    [XmlArrayItem("LessThan", typeof(XLessThanRule))]
    [XmlArrayItem("GreaterThan", typeof(XGreaterThanRule))]
    [XmlArrayItem("GreaterThanOrEqual", typeof(XGreaterThanOrEqualRule))]
    [XmlArrayItem("LessThanOrEqual", typeof(XLessThanOrEqualRule))]
    [XmlArrayItem("Equal", typeof(XEqualRule))]
    [XmlArrayItem("NotEqual", typeof(XNotEqualRule))]
    [XmlArrayItem("Length", typeof(XLengthRule))]
    [XmlArrayItem("NotEmpty", typeof(XNotEmptyRule))]
    [XmlArrayItem("NotNull", typeof(XNotNullRule))]
    public List<XRule> PostConditions { get; set; }
}