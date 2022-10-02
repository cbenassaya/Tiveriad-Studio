using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XParameterExtension
{
    public static ParameterCodeBuilder ToBuilder(this XParameter property)
    {
        InternalTypeCodeBuilder typeCodeBuilder;

        if (property.IsCollection)
        {
            Code.CreateInternalType(ComplexTypes.IENUMERABLE);
            typeCodeBuilder = Code.CreateInternalType(ComplexTypes.IENUMERABLE);
            typeCodeBuilder.WithGenericArgument(property.Type.ToBuilder());
        }
        else
        {
            typeCodeBuilder = property.Type.ToBuilder();
        }

        return Code.CreateParameter().WithType(typeCodeBuilder.Build()).WithName(property.Name);
    }
}