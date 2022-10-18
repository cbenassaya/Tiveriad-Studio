using Tiveriad.Pipelines;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.Extensions;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public class EntityBuilderRequestHandler : IRequestHandler<EntityBuilderRequest, ClassCodeBuilder>
{
    public Task<ClassCodeBuilder> Handle(EntityBuilderRequest request, CancellationToken cancellationToken)
    {
        var entity = request.Entity;
        var ids = entity.GetIds();
        var classBuilder = Code.CreateClass(entity.Name);
        if (entity.BaseType != null)
            classBuilder.WithInheritedClass(Code.CreateInternalType(entity.BaseType)
                .Build());

        classBuilder.WithImplementedInterface(
            Code
                .CreateInternalType(ComplexTypes.IENTITY)
                .WithGenericArguments(ids.Select(x=>Code.CreateInternalType(x.Type)).ToList()));

        if (entity.Persistence is { IsAuditable: true })
            classBuilder.WithImplementedInterface(
                Code
                    .CreateInternalType(ComplexTypes.IAUDITABLE)
                    .WithGenericArguments(ids.Select(x=>Code.CreateInternalType(x.Type)).ToList()));

        classBuilder
            .WithNamespace(entity.Namespace)
            .WithReference(entity)
            .WithProperties(
                entity.GetIds().Select(x => x.ToBuilder())
            )
            .WithProperties(
                entity.GetProperties().Select(x => x.ToBuilder())
            )
            .WithProperties(
                entity.GetManyToOneRelationShips().Select(x => x.ToBuilder())
            )
            .WithProperties(
                entity.GetManyToManyRelationShips().Select(x => x.ToBuilder())
            )
            .WithProperties(
                entity.GetOneToManyRelationShips().Select(x => x.ToBuilder())
            );
        return Task.FromResult(classBuilder);
    }
}