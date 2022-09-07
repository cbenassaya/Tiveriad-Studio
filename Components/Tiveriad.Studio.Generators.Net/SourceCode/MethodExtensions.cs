using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.SourceCode;

public static class MethodExtensions
{
    public static string ToSourceCode(this Method item)
    {
        var codeBuilder = CodeBuilder.Instance();

        codeBuilder.Append(
            $"{CodeBuilder.Instance().Append(item.Attributes, a => a.ToSourceCode(), CodeBuilder.Separator.EmptySpace)}");
        if (item.IsConstructor)
        {
            codeBuilder.Append($"{item.AccessModifier.ToSourceCode()}  {item.Parent.ValueOrFailure().Name.ValueOrFailure()}");
        }
        else
        {
            if (!item.ReturnType.HasValue)
            {
                codeBuilder.If(() => item.IsAsync).Append($"{item.AccessModifier.ToSourceCode()} async Task {item.Name.ValueOrFailure()}");
                codeBuilder.If(() => !item.IsAsync).Append($"{item.AccessModifier.ToSourceCode()} void {item.Name.ValueOrFailure()}");
            }
            else
            {
                codeBuilder.If(() => item.IsAsync)
                    .Append($"public async {item.ReturnType.ValueOrFailure().ToSourceCode()} {item.Name.ValueOrFailure()}");
                codeBuilder.If(() => !item.IsAsync)
                    .Append($"public {item.ReturnType.ValueOrFailure().ToSourceCode()} {item.Name.ValueOrFailure()}");
            }
        }

        codeBuilder.Append("(");
        codeBuilder.Append(item.Parameters, x => $"{x.ToSourceCode()}",
            CodeBuilder.Separator.Combine(CodeBuilder.Separator.Comma, CodeBuilder.Separator.WhiteSpace));
        codeBuilder.Append(")");
        codeBuilder.Append("{");
        codeBuilder.If(() => item.Body.HasValue).Append(()=>item.Body.ValueOrFailure());
        codeBuilder.Append("}");
        
        return codeBuilder.ToString();
    }
}