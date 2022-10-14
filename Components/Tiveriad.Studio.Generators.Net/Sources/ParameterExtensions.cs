using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class ParameterExtensions
{
    public static string ToSourceCode(this Parameter item)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"{CodeBuilder.Instance().Append(item.Attributes, a => a.ToSourceCode(), CodeBuilder.Separator.EmptySpace)}");
        codeBuilder.Append($"{item.Type.ValueOrFailure().ToSourceCode()} {item.Name.ValueOrFailure()}");
        return codeBuilder.ToString();
    }
}