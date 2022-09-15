using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class InternalTypeExtensions
{
    public static string ToSourceCode(this InternalType item)
    {
        var builder = CodeBuilder.Instance();

        builder.Append(item.Name.ValueOrFailure());

        if (item.GenericArguments.Any())
            builder
                .Append("<")
                .Append(item.GenericArguments, argument => argument.ToSourceCode(), CodeBuilder.Separator.Comma)
                .Append(">");
        return builder.ToString();
    }
}