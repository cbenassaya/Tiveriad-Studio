using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XConstraintExtensions
{
    public static AttributeBuilder ToBuilder(this XMaxLengthConstraint constraint)
    {
        return Code.CreateAttribute()
            .WithType(NComplexTypes.MAXLENGTHATTRIBUTE)
            .WithAttributeArgument(Code.CreateAttributeArgument().WithValue(constraint.Max.ToString()));
    }

    public static AttributeBuilder ToBuilder(this XMinLengthConstraint constraint)
    {
        return Code.CreateAttribute()
            .WithType(NComplexTypes.MINLENGTHATTRIBUTE)
            .WithAttributeArgument(Code.CreateAttributeArgument().WithValue(constraint.Min.ToString()));
    }

    public static AttributeBuilder ToBuilder(this XRequiredConstraint constraint)
    {
        return Code.CreateAttribute()
            .WithType(NComplexTypes.REQUIREDATTRIBUTE);
    }

    public static AttributeBuilder ToBuilder(this XIsUniqueConstraint constraint)
    {
        return Code.CreateAttribute()
            .WithType(NComplexTypes.ISUNIQUEATTRIBUTE);
    }
}