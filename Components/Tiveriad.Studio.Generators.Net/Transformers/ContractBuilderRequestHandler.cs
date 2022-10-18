using Tiveriad.Pipelines;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.Extensions;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public class ContractBuilderRequestHandler : IRequestHandler<ContractBuilderRequest, ClassCodeBuilder>
{
    public Task<ClassCodeBuilder> Handle(ContractBuilderRequest request, CancellationToken cancellationToken)
    {
        var contract = request.Contract;
        var classBuilder = Code.CreateClass(contract.Name)
            .WithNamespace(contract.Namespace)
            .WithReference(contract)
            .WithProperties(
                contract.GetProperties().Select(x => x.ToBuilder())
            );
        return Task.FromResult(classBuilder);
    }
}