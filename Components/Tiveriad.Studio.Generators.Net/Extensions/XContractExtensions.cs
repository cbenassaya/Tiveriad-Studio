using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Builders;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XContractExtensions
{
    public static ClassBuilder ToBuilder(this XContract contract)
    {
        var classBuilder = Code.CreateClass(contract.Name)
            .WithNamespace(contract.Namespace)
            .WithProperties(
                contract.GetProperties().Select(x => ((XProperty)x).ToBuilder())
            );
        return classBuilder;
    }
}