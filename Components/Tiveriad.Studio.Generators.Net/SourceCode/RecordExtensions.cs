using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.SourceCode;



public static class RecordExtensions
{
    public static string ToSourceCode(this Record item)
    {
        var builder = CodeBuilder.Instance();

        return builder
            //.Append($"{CodeBuilder.Instance().Append(item.Attributes, a => a.GetAttributeDeclaration(), CodeBuilder.Separator.EmptySpace)}")
            .Append($"{item.AccessModifier.ToSourceCode()} record {item.Name.ValueOrFailure()} (")
            .Append(item.Parameters, x => $"{x.ToSourceCode()}", CodeBuilder.Separator.Combine(CodeBuilder.Separator.Comma,CodeBuilder.Separator.WhiteSpace))
            .Append(")")
            .If(() => item.ImplementedInterfaces.Any()).Append(":")
            .Append( item.ImplementedInterfaces, @interface => ((InternalType) @interface).ToSourceCode(), CodeBuilder.Separator.Comma)
            .Append(";")
            .ToString();
    }
}