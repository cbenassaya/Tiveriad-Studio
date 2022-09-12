using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Services;

public interface IXTypeService
{
    public IQueryable<TElement> AsQueryable<TElement>() where TElement : XType;
    public IQueryable<XType> AsQueryable();
    public IEnumerable<XType> GetAll();
    public XType Get(string definition);
    public void Add(XType type);
    public bool Exists(XType type);
}