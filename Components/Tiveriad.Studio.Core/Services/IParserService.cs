using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Services;

public interface IParserService
{
    public XProject Parse(Stream stream);
}