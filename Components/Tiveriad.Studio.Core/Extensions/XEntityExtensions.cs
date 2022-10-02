using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Extensions;

public static class XEntityExtensions
{
    public static XMemberBase GetFromPath(this XEntity classifier, string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        var pathItems = path.Split('.');
        var item = pathItems[0];

        var nProperty = classifier.Properties.FirstOrDefault(x => x.Name == item);

        if (nProperty == null)
        {
            var relationShip = classifier.GetManyToOneRelationShips().FirstOrDefault(x => x.Name == item);
            var nClassifier = relationShip?.Classifier as XEntity;

            if (nClassifier == null) return classifier.BaseType?.GetFromPath(path);

            if (pathItems.Length > 1)
            {
                var subPath = path.Substring(item.Length + 1, path.Length - item.Length - 1);

                var result = nClassifier.BaseType?.GetFromPath(subPath);
                return result ?? nClassifier?.GetFromPath(subPath);
            }
        }

        return nProperty;
    }

    public static IList<XManyToOne> GetManyToOneRelationShips(this XEntity classifier)
    {
        return classifier.RelationShips.Where(x => x is XManyToOne).Cast<XManyToOne>().ToList();
    }

    public static IList<XOneToMany> GetOneToManyRelationShips(this XEntity classifier)
    {
        return classifier.RelationShips.Where(x => x is XOneToMany).Cast<XOneToMany>().ToList();
    }

    public static IList<XManyToMany> GetManyToManyRelationShips(this XEntity classifier)
    {
        return classifier.RelationShips.Where(x => x is XManyToMany).Cast<XManyToMany>().ToList();
    }

    public static IList<XEntity> GetChildren(this XEntity xEntity)
    {
        var values = xEntity.GetProject().GetChildren<XEntity>();
        return values.Where(x => x.BaseType != null && x.BaseType.Equals(xEntity)).ToList();
    }

    public static IEnumerable<XEntity> GetFunctionalDependencies(this XEntity entity)
    {
        if (entity == null) return Enumerable.Empty<XEntity>();
        return entity.RelationShips
            .Where(x => x is XManyToOne)
            .Select(x => x.Type).Cast<XEntity>().ToList()
            .Where(x => x.IsBusinessEntity).AsEnumerable();
    }
}