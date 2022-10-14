using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class ClassExtensions
{
    public static string ToSourceCode(this Class item)
    {
        var builder = CodeBuilder.Instance();
        var parentAndContracts = new List<InternalType>();
        if (item.InheritedClass != null)
            parentAndContracts.Add(item.InheritedClass);
        parentAndContracts.AddRange(item.ImplementedInterfaces);
        builder.Append(item.Dependencies, dependency => $"using {dependency};", CodeBuilder.Separator.EmptySpace);
        builder.Append($"namespace {item.Namespace};");
        builder.Append(
            $"{CodeBuilder.Instance().Append(item.Attributes, a => a.ToSourceCode(), CodeBuilder.Separator.EmptySpace)}");
        builder.Append($"{item.AccessModifier.ToSourceCode()} class {item.Name}");
        builder.If(() => parentAndContracts.Any()).Append(":");
        builder.If(() => item.InheritedClass != null).Append(() => item.InheritedClass.ToSourceCode());
        builder.If(() => item.InheritedClass != null && item.ImplementedInterfaces.Any()).Append(", ");
        builder.Append(item.ImplementedInterfaces, @interface => @interface.ToSourceCode(),
            CodeBuilder.Separator.Comma);
        builder.Append("{");
        builder.Append(item.Fields, field => field.ToSourceCode(), CodeBuilder.Separator.EmptySpace);
        builder.Append(item.Properties, property => property.ToSourceCode(), CodeBuilder.Separator.EmptySpace);
        builder.Append(item.Methods, method => method.ToSourceCode(), CodeBuilder.Separator.EmptySpace);
        builder.Append("}");
        return builder.ToString();
    }
}