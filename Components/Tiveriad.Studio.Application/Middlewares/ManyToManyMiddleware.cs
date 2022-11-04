using MongoDB.Bson;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class ManyToManyMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IXTypeService _typeService;

    public ManyToManyMiddleware(IXTypeService typeService)
    {
        _typeService = typeService;
    }

    public Task Run(PipelineContext context, PipelineModel model)
    {
        Traverse(model.Project);
        return Task.CompletedTask;
    }

    protected override bool ApplyIf(XElementBase value)
    {
        return value is XEntity;
    }

    protected override void DoApply(XElementBase value)
    {
        if (value is XEntity entity)
            DoApply(entity);
    }

    private void DoApply(XEntity entity)
    {
        foreach (var manyToManyRelationShip in entity.GetManyToManyRelationShips())
        {
            var relationShips = new List<XRelationShip>();
            var properties = new List<XPropertyBase>();
            var persistence = new XPersistence();
            var linkEntity = new XEntity
            {
                Id = ObjectId.GenerateNewId(),
                Name = manyToManyRelationShip.Name ?? $"{entity.Name}{manyToManyRelationShip.Type.Name}",
                PluralName = manyToManyRelationShip.PluralName ??
                             $"{entity.Name}{manyToManyRelationShip.Type.PluralName}",
                Package = entity.Package,
                Namespace = entity.Namespace,
                Properties = properties,
                RelationShips = relationShips,
                Persistence = persistence,
                IsBusinessEntity = false
            };

            persistence.Id = ObjectId.GenerateNewId();
            persistence.Name = $"L_{linkEntity.Name}";
            persistence.Entity = linkEntity;

            relationShips.Add(new XManyToOne
            {
                Id = ObjectId.GenerateNewId(),
                Name = $"{entity.Name}",
                Type = entity,
                Classifier = linkEntity,
                TypeReference = $"{entity.Name}"
            });

            relationShips.Add(new XManyToOne
            {
                Id = ObjectId.GenerateNewId(),
                Name = $"{manyToManyRelationShip.Type.Name}",
                Type = manyToManyRelationShip.Type,
                Classifier = linkEntity,
                TypeReference = $"{manyToManyRelationShip.Type.Name}"
            });

            properties.AddRange(entity.GetIds()
                .Select(xId => new XId
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = $"{entity.Name}{xId.Name}",
                    Type = xId.Type,
                    Classifier = linkEntity,
                    TypeReference = linkEntity.Name,
                    Constraints = xId.Constraints,
                    Displayed = false
                }));

            properties.AddRange(((XEntity)manyToManyRelationShip.Type).GetIds()
                .Select(xId => new XId
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = $"{manyToManyRelationShip.Type.Name}{xId.Name}",
                    Type = xId.Type,
                    Classifier = linkEntity,
                    TypeReference = linkEntity.Name,
                    Constraints = xId.Constraints,
                    Displayed = false
                }));

            entity.RelationShips.Add(new XOneToMany
            {
                Id = ObjectId.GenerateNewId(),
                Name = linkEntity.PluralName,
                Type = linkEntity,
                Classifier = entity,
                TypeReference = linkEntity.Name
            });

            ((XEntity)manyToManyRelationShip.Type).RelationShips.Add(new XOneToMany
            {
                Id = ObjectId.GenerateNewId(),
                Name = linkEntity.PluralName,
                Type = linkEntity,
                Classifier = manyToManyRelationShip.Type as XClassifier,
                TypeReference = linkEntity.Name
            });

            _typeService.Add(linkEntity);
        }

        var cleanList = entity.RelationShips.Where(x => x is not XManyToMany).ToList().Cast<XRelationShip>();
        entity.RelationShips = new List<XRelationShip>();
        entity.RelationShips.AddRange(cleanList);
    }
}