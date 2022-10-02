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
        var classBuilder = Code.CreateClass(entity.Name);
        if (entity.BaseType != null)
            classBuilder.WithInheritedClass(Code.CreateInternalType(entity.BaseType.Name, entity.BaseType.Namespace)
                .Build());

        classBuilder.WithImplementedInterface(
            Code
                .CreateInternalType(ComplexTypes.IENTITY)
                .WithGenericArgument(Code.CreateInternalType().WithName(entity.Name).WithNamespace(entity.Namespace)));

        if (entity.Persistence is { IsAuditable: true })
            classBuilder.WithImplementedInterface(
                Code
                    .CreateInternalType(ComplexTypes.IAUDITABLE)
                    .WithGenericArgument(
                        Code.CreateInternalType().WithName(entity.Name).WithNamespace(entity.Namespace)));

        classBuilder
            .WithNamespace(entity.Namespace)
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