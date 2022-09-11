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
            Code.CreateInternalType(ComplexTypes.IENUMERABLE);
            typeBuilder = Code.CreateInternalType(ComplexTypes.IENUMERABLE);
            typeBuilder.WithGenericArgument(XTypeExtensions.ToBuilder(property.Type));
        }
        else
        {
            typeBuilder = XTypeExtensions.ToBuilder(property.Type);
        }

        return Code.CreateParameter().WithType(typeBuilder.Build()).WithName(property.Name);
    }
}