using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class InternalTypeBuilderExtensions
{
    public static InternalTypeBuilder ToBuilder(this XType type)
    {
        var ntype = NDataTypes.Types.FirstOrDefault(x => x.Reference.HasValue && x.Reference.Contains(type));

        return ntype == null
            ? Code.CreateInternalType().WithName(type.Name).WithNamespace(type.Namespace)
            : Code.CreateInternalType(ntype);
    }
}