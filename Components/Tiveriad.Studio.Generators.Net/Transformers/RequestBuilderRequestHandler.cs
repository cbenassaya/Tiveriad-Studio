using Tiveriad.Pipelines;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.Extensions;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public class RequestBuilderRequestHandler : IRequestHandler<RequestBuilderRequest, RecordCodeBuilder>
{
    public Task<RecordCodeBuilder> Handle(RequestBuilderRequest request, CancellationToken cancellationToken)
    {
        var xAction = request.Action;
        var res = Code
            .CreateInternalType(ComplexTypes.IREQUEST);
        if (xAction.Response != null)
            res.WithGenericArgument(xAction.Response.Type.ToBuilder(xAction.BehaviourType));

        var recordBuilder = Code
            .CreateRecord($"{xAction.Name}Request")
            .WithNamespace(xAction.Namespace)
            .WithReference(xAction)
            .WithImplementedInterface(res);
        if (xAction.Parameters != null)
            recordBuilder.WithParameters(xAction.Parameters.Select(x => x.ToBuilder()).ToArray());

        return Task.FromResult(recordBuilder);
    }
}