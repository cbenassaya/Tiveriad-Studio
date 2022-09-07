using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Extensions;

public static class XRelationShipExtensions
{
    public static XRelationShip GetReverseRelation(this XRelationShip relationShip)
    {
        var to = relationShip.Type as XEntity;
        XRelationShip value = null;

        if (relationShip is XOneToMany)
            value = to?.RelationShips.FirstOrDefault(p =>
                p is XManyToOne &&
                relationShip.Classifier.Name == p.Type.Name);
        else if (relationShip is XManyToMany)
            value = to?.RelationShips.FirstOrDefault(p =>
                p is XManyToMany &&
                relationShip.Classifier.Name == p.Type.Name);
        return value;
    }
}