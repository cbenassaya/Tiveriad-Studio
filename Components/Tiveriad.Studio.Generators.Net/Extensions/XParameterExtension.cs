using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XParameterExtension
{
    public static ParameterBuilder ToBuilder(this XParameter property)
    {
        InternalTypeBuilder typeBuilder;

        if (property.IsCollection)
        {
            Code.CreateInternalType(NComplexTypes.IENUMERABLE);
            typeBuilder = Code.CreateInternalType(NComplexTypes.IENUMERABLE);
            typeBuilder.WithGenericArgument(InternalTypeBuilderExtensions.ToBuilder(property.Type));
        }
        else
        {
            typeBuilder = InternalTypeBuilderExtensions.ToBuilder(property.Type);
        }

        return Code.CreateParameter().WithType(typeBuilder.Build()).WithName(property.Name);
    }
}