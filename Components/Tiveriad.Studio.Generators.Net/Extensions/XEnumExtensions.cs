using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XEnumExtensions
{
    public static EnumCodeBuilder ToBuilder(this XEnum @enum)
    {
        return Code.CreateEnum(@enum.Name)
            .WithNamespace(@enum.Namespace)
            .WithMembers(@enum.Values.Select(x => Code.CreateEnumMember(x)).ToList());
    }
}