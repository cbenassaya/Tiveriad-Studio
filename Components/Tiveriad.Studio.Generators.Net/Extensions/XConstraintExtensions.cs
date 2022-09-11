using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XConstraintExtensions
{
    public static AttributeBuilder ToBuilder(this XMaxLengthConstraint constraint)
    {
        return Code.CreateAttribute()
            .WithType(ComplexTypes.MAXLENGTHATTRIBUTE)
            .WithAttributeArgument(Code.CreateAttributeArgument().WithValue(constraint.Max.ToString()));
    }

    public static AttributeBuilder ToBuilder(this XMinLengthConstraint constraint)
    {
        return Code.CreateAttribute()
            .WithType(ComplexTypes.MINLENGTHATTRIBUTE)
            .WithAttributeArgument(Code.CreateAttributeArgument().WithValue(constraint.Min.ToString()));
    }

    public static AttributeBuilder ToBuilder(this XRequiredConstraint constraint)
    {
        return Code.CreateAttribute()
            .WithType(ComplexTypes.REQUIREDATTRIBUTE);
    }

    public static AttributeBuilder ToBuilder(this XIsUniqueConstraint constraint)
    {
        return Code.CreateAttribute()
            .WithType(ComplexTypes.ISUNIQUEATTRIBUTE);
    }
}