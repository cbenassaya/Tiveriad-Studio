using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Extensions;

public static class XClassifierExtensions
{
    public static XProject GetProject(this XClassifier type)
    {
        return type.Package?.GetProject();
    }
    
    public static IList<XId> GetIds(this XClassifier classifier)
    {
        return classifier.Properties.Where(x => x is XId).Cast<XId>().ToList();
    }
    
    public static IList<XProperty> GetProperties(this XClassifier classifier)
    {
        return classifier.Properties.Where(x => x is XProperty).Cast<XProperty>().ToList();
    }
    
    public static XMemberBase GetFromPath(this XClassifier classifier, string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        var pathItems = path.Split('.');
        var item = pathItems[0];

        var nProperty = classifier.Properties.FirstOrDefault(x => x.Name == item);

        if (nProperty == null)
            if (classifier is XEntity xEntity)
                return xEntity.BaseType?.GetFromPath(path);

        if (nProperty.Type is XClassifier nClassifier && pathItems.Length > 1)
        {
            var subPath = path.Substring(item.Length + 1, path.Length - item.Length - 1);
            XMemberBase result = null;
            if (nClassifier is XEntity xEntity) result = xEntity.BaseType?.GetFromPath(subPath);
            return result ?? nClassifier?.GetFromPath(subPath);
        }

        return nProperty;
    }
}