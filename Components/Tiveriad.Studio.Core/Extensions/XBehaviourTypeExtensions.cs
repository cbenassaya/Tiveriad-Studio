using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Extensions;

public static class XBehaviourTypeExtensions
{
    public static bool IsQuery(this XBehaviourType type)
    {
        return type switch
        {
            XBehaviourType.Delete => false,
            XBehaviourType.Query => true,
            XBehaviourType.GetMany => true,
            XBehaviourType.GetOne => true,
            XBehaviourType.SaveOrUpdate => true,
            XBehaviourType.Command => false
        };
    }

    public static string ToCqrs(this XBehaviourType type)
    {
        return type.IsQuery() switch
        {
            true => "Query",
            false => "Command"
        };
    }
}