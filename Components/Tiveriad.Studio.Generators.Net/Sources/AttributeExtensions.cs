using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;
using Attribute = Tiveriad.Studio.Generators.Models.Attribute;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class AttributeExtensions
{
    public static string ToSourceCode(this Attribute item)
    {
        var builder = CodeBuilder.Instance();

        return builder
            .Append($"[{item.InternalType.Name}")
            .Append(CodeBuilder.Instance()
                .If(() => item.AttributeArguments.Any()).Append("(")
                .If<AttributeArgument>(x => !string.IsNullOrEmpty(x.Value)).Append(item.AttributeArguments,
                    x => x.Value, CodeBuilder.Separator.Comma)
                .If(() => item.AttributeArguments.Any()).Append(")")
                .ToString())
            .Append("]")
            .ToString();
    }
}