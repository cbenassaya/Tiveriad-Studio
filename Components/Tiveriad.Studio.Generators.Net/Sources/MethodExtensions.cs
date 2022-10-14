using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class MethodExtensions
{
    public static string ToSourceCode(this Method item)
    {
        var codeBuilder = CodeBuilder.Instance();

        codeBuilder.Append(
            $"{CodeBuilder.Instance().Append(item.Attributes, a => a.ToSourceCode(), CodeBuilder.Separator.EmptySpace)}");
        if (item.IsConstructor)
        {
            codeBuilder.Append(
                $"{item.AccessModifier.ToSourceCode()}  {item.Parent.Name}");
        }
        else
        {
            if (item.ReturnType == null)
            {
                codeBuilder.If(() => item.IsAsync)
                    .Append($"{item.AccessModifier.ToSourceCode()} async Task {item.Name}");
                codeBuilder.If(() => !item.IsAsync)
                    .Append($"{item.AccessModifier.ToSourceCode()} void {item.Name}");
            }
            else
            {
                codeBuilder.If(() => item.IsAsync)
                    .Append(
                        $"public async {item.ReturnType.ToSourceCode()} {item.Name}");
                codeBuilder.If(() => !item.IsAsync)
                    .Append($"public {item.ReturnType.ToSourceCode()} {item.Name}");
            }
        }

        codeBuilder.Append("(");
        codeBuilder.Append(item.Parameters, x => $"{x.ToSourceCode()}",
            CodeBuilder.Separator.Combine(CodeBuilder.Separator.Comma, CodeBuilder.Separator.WhiteSpace));
        codeBuilder.Append(")");
        codeBuilder.Append("{");
        codeBuilder.If(() => !string.IsNullOrEmpty(item.Body)).Append(() =>
            CodeBuilder.Separator.NewLine + item.Body + CodeBuilder.Separator.NewLine);
        codeBuilder.Append("}");

        return codeBuilder.ToString();
    }
}