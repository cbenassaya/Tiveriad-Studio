using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Exceptions;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Services;

public class XTypeService:IXTypeService
{
    private readonly IList<XType> _xtypes = new List<XType>();

    public XTypeService()
    {
        XDataTypes.Types.ForEach(x => _xtypes.Add(x));
    }

    public IQueryable<TElement> AsQueryable<TElement>() where TElement : XType
    {
        var values = _xtypes.Where(x => x.GetType() == typeof(TElement)).ToArray();
        return values.Cast<TElement>().AsQueryable();
    }

    public IQueryable<XType> AsQueryable()
    {
        return _xtypes.AsQueryable();
    }

    public IEnumerable<XType> GetAll()
    {
        return _xtypes;
    }

    public XType Get(string definition)
    {
        var name = string.Empty;
        var @namespace = string.Empty;

        IEnumerable<XType> results = Array.Empty<XType>();

        if (definition.Contains(","))
        {
            var items = definition.Split(",");
            name = items[0];
            @namespace = items[1];
        }
        else if (definition.Contains("."))
        {
            var items = definition.Split(".");
            name = items[^1];
            @namespace = definition.Substring(0, definition.Length - name.Length - 1);
        }
        else
        {
            name = definition;
        }

        if (string.IsNullOrEmpty(@namespace))
            results = AsQueryable().Where(x => x.Name == name).ToList();
        else
            results = AsQueryable().Where(x => x.Name == name && x.Namespace == @namespace).ToList();

        if (!results.Any())
            throw new TiveriadStudioException($"No type for {definition} definition");

        if (results.Count() > 1)
            throw new TiveriadStudioException($"Ambiguous search for {definition} definition");

        return results.FirstOrDefault();
    }

    public void Add(XType type)
    {
        if (!Exists(type))
            _xtypes.Add(type);
    }

    public bool Exists(XType type)
    {
        return AsQueryable().Any(x => x.Name == type.Name && x.Namespace == type.Namespace);
    }
}