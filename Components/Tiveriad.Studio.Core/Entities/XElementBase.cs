using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tiveriad.Repositories;

namespace Tiveriad.Studio.Core.Entities;

[XmlType("ElementBase")]
public class XElementBase : IEntity<ObjectId>, IAuditable<ObjectId>
{
    [XmlAttribute("Description")]
    [MaxLength(50)]
    public string Description { get; set; }

    [XmlAttribute("Version")]
    [MaxLength(50)]
    public string Version { get; set; }

    [XmlIgnore] public ObjectId CreatedBy { get; set; }
    [XmlIgnore] public ObjectId LastModifiedBy { get; set; }
    [XmlIgnore] public DateTime Created { get; set; }
    [XmlIgnore] public DateTime? LastModified { get; set; }

    [BsonId] [XmlIgnore] public ObjectId Id { get; set; }
}