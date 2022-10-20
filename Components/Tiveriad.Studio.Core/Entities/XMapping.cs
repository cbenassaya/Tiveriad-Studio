using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Tiveriad.Studio.Core.Attributes;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("Mapping")]
public class XMapping : XElementBase
{


    private string _fromReference;
    private string _toReference;

    [XmlAttribute("From")]
    public string FromReference
    {
        get => string.IsNullOrEmpty(_fromReference) ? From?.ToString() ?? string.Empty : _fromReference;
        set => _fromReference = value;
    }

    [BsonIgnore]
    [XmlIgnore]
    [InjectWith("ActionReference")]
    public XType From { get; set; }


    [XmlAttribute("To")]
    public string ToReference
    {
        get => string.IsNullOrEmpty(_toReference) ? To?.ToString() ?? string.Empty : _toReference;
        set => _toReference = value;
    }

    [BsonIgnore]
    [XmlIgnore]
    [InjectWith("ToReference")]
    public XType To { get; set; }
    
    protected bool Equals(XMapping other)
    {
        return From.Equals(other.From) && To.Equals(other.To);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((XMapping)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To);
    }

    public static bool operator ==(XMapping? left, XMapping? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(XMapping? left, XMapping? right)
    {
        return !Equals(left, right);
    }
}