using System.Runtime.CompilerServices;
using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Extensions;

public static class XActionExtensions
{
    public static XMemberBase GetFromPath(this XAction classifier, string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        var pathItems = path.Split('.');
        var item = pathItems[0];

        var xParameter = classifier.Parameters.FirstOrDefault(x => x.Name == item);

        if (xParameter == null) return null;

        if (xParameter.Type is XClassifier nClassifier && pathItems.Length > 1)
        {
            var subPath = path.Substring(item.Length + 1, path.Length - item.Length - 1);
            XMemberBase result = null;
            if (nClassifier is XEntity xEntity) result = xEntity.BaseType?.GetFromPath(subPath);
            return result ?? nClassifier?.GetFromPath(subPath);
        }

        return xParameter;
    } 
    
    public static List<XRule> GetPreConditions(this XAction xAction)
    {
        return xAction.PreConditions ?? new List<XRule>();
    }

    public static void Add(this XAction xAction, XRule xRule)
    {
        if (xAction.PreConditions == null)
            xAction.PreConditions = new List<XRule>();

        xAction.PreConditions.Add(xRule);
    }
}