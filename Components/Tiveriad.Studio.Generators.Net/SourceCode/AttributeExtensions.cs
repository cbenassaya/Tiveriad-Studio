using System.Linq;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Sources;
using Tiveriad.Studio.Generators.Models;
using Attribute = Tiveriad.Studio.Generators.Models.Attribute;

namespace Tiveriad.Studio.Generators.Net.SourceCode;

public static class AttributeExtensions
{
    public static string ToSourceCode(this Attribute item)
    {
        
        var builder = CodeBuilder.Instance();

        return builder
            .Append($"[{item.InternalType.ValueOrFailure().Name.ValueOrFailure()}")
            .Append(CodeBuilder.Instance()
                .If(() => item.AttributeArguments.Any()).Append("(")
                .If<AttributeArgument>(x => x.Value.HasValue).Append(item.AttributeArguments, x => x.Value.ValueOrFailure(), CodeBuilder.Separator.Comma)
                .If(() => item.AttributeArguments.Any()).Append(")")
                .ToString())
            .Append("]")
            .ToString();
    }
}