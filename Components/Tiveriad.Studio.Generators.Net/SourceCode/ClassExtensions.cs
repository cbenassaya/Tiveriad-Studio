using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.SourceCode;

public static class ClassExtensions
{
    public static string ToSourceCode(this Class item)
    {
        var builder = CodeBuilder.Instance();
        var parentAndContracts = new List<InternalType>();
        if (item.InheritedClass.HasValue)
            parentAndContracts.Add(item.InheritedClass.ValueOrFailure());
        parentAndContracts.AddRange(item.ImplementedInterfaces);
        return builder
            .Append($"namespace {item.Namespace.ValueOrFailure()};")
            .Append($"{CodeBuilder.Instance().Append(item.Attributes, a => a.ToSourceCode(), CodeBuilder.Separator.EmptySpace)}")
            .Append($"{item.AccessModifier.ToSourceCode()} class {item.Name.ValueOrFailure()}")
            .If(() => parentAndContracts.Any()).Append(":")
            .Append( item.ImplementedInterfaces, @interface => ((InternalType) @interface).ToSourceCode(), CodeBuilder.Separator.Comma)
            .Append("{")
            .Append( item.Fields, @field => field.ToSourceCode(), CodeBuilder.Separator.EmptySpace)
            .Append( item.Properties, @property => property.ToSourceCode(), CodeBuilder.Separator.EmptySpace)
            .Append( item.Methods, @method => method.ToSourceCode(), CodeBuilder.Separator.EmptySpace)
            .Append("}")
            .ToString();
    }
}