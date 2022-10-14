using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Net.Sources;

public static class EnumerationMemberTypeExtensions
{
    public static string ToSourceCode(this EnumerationMember item)
    {
        return item.Name;
    }
}