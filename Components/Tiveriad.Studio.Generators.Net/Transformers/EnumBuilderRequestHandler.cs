using Tiveriad.Pipelines;
using Tiveriad.Studio.Generators.Builders;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public class EnumBuilderRequestHandler : IRequestHandler<EnumBuilderRequest, EnumCodeBuilder>
{
    public Task<EnumCodeBuilder> Handle(EnumBuilderRequest request, CancellationToken cancellationToken)
    {
        var @enum = request.Enum;
        var classBuilder = Code.CreateEnum(@enum.Name)
            .WithNamespace(@enum.Namespace)
            .WithMembers(@enum.Values.Select(x => Code.CreateEnumMember(x)).ToList());
        return Task.FromResult(classBuilder);
    }
}