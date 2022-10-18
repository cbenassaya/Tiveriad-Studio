using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XTypeExtensions
{
    public static InternalTypeCodeBuilder ToBuilder(this XType type, XBehaviourType behaviourType = XBehaviourType.Command)
    {
        var ntype = DataTypes.Types.FirstOrDefault(x => x.Reference != null && x.Reference.Equals(type));

        if (ntype!=null)
            return Code.CreateInternalType(ntype);

        if (behaviourType is XBehaviourType.GetMany or XBehaviourType.Query)
        {
            return Code.CreateInternalType(ComplexTypes.IENUMERABLE).WithGenericArgument(Code.CreateInternalType(type));
        }
        return Code.CreateInternalType(type);
    }
}