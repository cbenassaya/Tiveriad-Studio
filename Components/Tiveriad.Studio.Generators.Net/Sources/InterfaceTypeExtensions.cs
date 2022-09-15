using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class InterfaceTypeExtensions
{
    public static string ToSourceCode(this Interface item)
    {
        var builder = CodeBuilder.Instance();

        return builder
            .Append($"namespace {item.Namespace.ValueOrFailure()};")
            //.Append($"{CodeBuilder.Instance().Append(item.Attributes, a => a.ToSourceCode(), CodeBuilder.Separator.EmptySpace)}")
            .Append($"{item.AccessModifier.ToSourceCode()} class {item.Name.ValueOrFailure()}")
            .If(() => item.ExtentedInterfaces.Any()).Append(":")
            .Append( item.ExtentedInterfaces, @interface => ((InternalType) @interface).ToSourceCode(), CodeBuilder.Separator.Comma)
            .Append("{")
            .Append( item.Properties, @property => property.ToSourceCode(), CodeBuilder.Separator.EmptySpace)
            //.Append( item.Methods, @method => method.ToSourceCode(), CodeBuilder.Separator.EmptySpace)
            .Append("}")
            .ToString();
    }
}