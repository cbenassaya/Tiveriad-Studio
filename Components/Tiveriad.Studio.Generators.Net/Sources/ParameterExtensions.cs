using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class ParameterExtensions
{
    public static string ToSourceCode(this Parameter item)
    {
        return $"{item.Type.ValueOrFailure().ToSourceCode()} {item.Name.ValueOrFailure()}";
    }
}