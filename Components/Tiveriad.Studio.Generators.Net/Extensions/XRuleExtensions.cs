using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XRuleExtensions
{
    public static string GetRuleDeclaration(this XRule rule)
    {
        if (rule is XNotNullRule)
            return ((XNotNullRule)rule).GetRuleDeclaration();
        if (rule is XNotEmptyRule)
            return ((XNotEmptyRule)rule).GetRuleDeclaration();
        if (rule is XLengthRule)
            return ((XLengthRule)rule).GetRuleDeclaration();
        if (rule is XNotEqualRule)
            return ((XNotEqualRule)rule).GetRuleDeclaration();
        if (rule is XEqualRule)
            return ((XEqualRule)rule).GetRuleDeclaration();
        if (rule is XLessThanOrEqualRule)
            return ((XLessThanOrEqualRule)rule).GetRuleDeclaration();
        if (rule is XGreaterThanOrEqualRule)
            return ((XGreaterThanOrEqualRule)rule).GetRuleDeclaration();
        if (rule is XGreaterThanRule)
            return ((XGreaterThanRule)rule).GetRuleDeclaration();
        if (rule is XLessThanRule)
            return ((XLessThanRule)rule).GetRuleDeclaration();
        if (rule is XPredicateRule)
            return ((XPredicateRule)rule).GetRuleDeclaration();
        if (rule is XRegularExpressionRule)
            return ((XRegularExpressionRule)rule).GetRuleDeclaration();
        if (rule is XMaxLengthRule)
            return ((XMaxLengthRule)rule).GetRuleDeclaration();
        if (rule is XMinLengthRule)
            return ((XMinLengthRule)rule).GetRuleDeclaration();

        return string.Empty;
    }

    public static string GetRuleDeclaration(this XNotNullRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append(".NotNull();");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XNotEmptyRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append(".NotEmpty();");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XLengthRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".Length({rule.Min},{rule.Max});");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XNotEqualRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".NotEqual({rule.Value});");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XEqualRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".NotEqual({rule.Value});");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XLessThanOrEqualRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".LessThanOrEqualTo({rule.Value});");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XGreaterThanOrEqualRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".GreaterThanOrEqualTo({rule.Value});");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XGreaterThanRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".GreaterThan({rule.Value});");
        return codeBuilder.ToString();
    }


    public static string GetRuleDeclaration(this XLessThanRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".LessThan({rule.Value});");
        return codeBuilder.ToString();
    }


    public static string GetRuleDeclaration(this XPredicateRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".Must({rule.PredicateName});");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XRegularExpressionRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".Matches({rule.Expression});");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XMaxLengthRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".MaximumLength({rule.Max});");
        return codeBuilder.ToString();
    }

    public static string GetRuleDeclaration(this XMinLengthRule rule)
    {
        var codeBuilder = CodeBuilder.Instance();
        codeBuilder.Append($"RuleFor(x => x.{rule.Path})");
        codeBuilder.Append($".MinimumLength({rule.Min});");
        return codeBuilder.ToString();
    }
}