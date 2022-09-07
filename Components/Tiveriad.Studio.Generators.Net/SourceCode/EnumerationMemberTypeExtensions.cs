using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Net.SourceCode;

public static class EnumerationMemberTypeExtensions
{
    public static string ToSourceCode(this EnumerationMember item)
    {
        return item.Name.ValueOrFailure();
    }
}