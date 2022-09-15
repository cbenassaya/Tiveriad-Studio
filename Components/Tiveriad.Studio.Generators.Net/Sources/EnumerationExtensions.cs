using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class EnumerationExtensions
{
    public static string ToSourceCode(this Enumeration item)
    {
        var builder = CodeBuilder.Instance();

        return builder
            //.Append($"{CodeBuilder.Instance().Append(item.Attributes, a => a.GetAttributeDeclaration(), CodeBuilder.Separator.EmptySpace)}")
            .Append($"{item.AccessModifier.ToSourceCode()} enum {item.Name.ValueOrFailure()} {{")
            .Append(item.Members, x => $"{x.ToSourceCode()}", CodeBuilder.Separator.Combine(CodeBuilder.Separator.Comma,CodeBuilder.Separator.WhiteSpace))
            .Append("}")
            .ToString();
    }
}