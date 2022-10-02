using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class StringExtensions
{
    public static string NormalizeWhitespace(this string code)
    {
        var tree = CSharpSyntaxTree.ParseText(code);
        var root = tree.GetRoot().NormalizeWhitespace();
        var ret = root.ToFullString();
        return ret;
    }
}