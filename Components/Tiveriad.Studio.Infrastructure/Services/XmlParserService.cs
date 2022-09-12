using System.Xml.Serialization;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Infrastructure.Services;

public class XmlParserService:IParserService
{
    private readonly XmlSerializer _serializer = new(typeof(XProject));
    
    public XProject Parse(Stream stream)
    {
        ArgumentNullException.ThrowIfNull("stream");
        var project =_serializer.Deserialize(stream);
        ArgumentNullException.ThrowIfNull("project");
        return (XProject)project;
    }
}