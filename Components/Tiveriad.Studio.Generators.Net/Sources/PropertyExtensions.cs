using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class PropertyExtensions
{
    public static string ToSourceCode(this Property item)
    {
        var builder = CodeBuilder.Instance();

        return builder
            .Append(
                $"{CodeBuilder.Instance().Append(item.Attributes, a => a.ToSourceCode(), CodeBuilder.Separator.EmptySpace)}")
            .Append(
                $"{item.AccessModifier.ToSourceCode()} {item.Type.ValueOrFailure().ToSourceCode()} {item.Name.ValueOrFailure()} {{ get; set; }}")
            .ToString();
    }
}