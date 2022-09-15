using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class AccessModifierExtensions
{
    public static string ToSourceCode(this AccessModifier item)
        => item switch
        {
            AccessModifier.Public=>"public",
            AccessModifier.Private=>"private",
            AccessModifier.Internal=>"internal",
            AccessModifier.Protected=>"protected",
            _=>string.Empty
        };
}