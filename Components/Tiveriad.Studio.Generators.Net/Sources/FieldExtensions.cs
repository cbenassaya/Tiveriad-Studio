using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class FieldExtensions
{
    public static string ToSourceCode(this Field item)
    {
        var builder = CodeBuilder.Instance();

        return builder
            //.Append($"{CodeBuilder.Instance().Append(item.Attributes, a => a.GetAttributeDeclaration(), CodeBuilder.Separator.EmptySpace)}")
            .Append($"{item.AccessModifier.ToSourceCode()} ")
            .If<Field>(x => x.IsReadonly).Append(item, x => "readonly ")
            .Append($"{item.Type.ToSourceCode()} ")
            .Append($"{item.Name};")
            .ToString();
    }
}