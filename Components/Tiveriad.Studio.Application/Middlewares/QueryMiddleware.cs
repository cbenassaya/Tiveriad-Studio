using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class QueryMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IXTypeService _typeService;

    public QueryMiddleware(IXTypeService typeService)
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
        return value is XEntity { IsBusinessEntity: true };
    }

    protected override void DoApply(XElementBase value)
    {
        var entity = value as XEntity;
        var project = entity?.GetProject();
        if (project == null) return;
        var module = entity?.GetModule();
        if (module == null) return;

        var queriesPackage = module.Packages.FirstOrDefault(x => x.Name == "Queries");
        if (queriesPackage == null)
        {
            queriesPackage = new XPackage
            {
                Name = "Queries",
                Module = module
            };
            module.Packages.Add(queriesPackage);
        }

        var queryPackage = module.Packages.FirstOrDefault(x => x.Name == $"{entity.Name}Queries");
        if (queryPackage == null)
        {
            queryPackage = new XPackage
            {
                Name = $"{entity.Name}Queries",
                Parent = queriesPackage
            };
            queriesPackage.Add(queryPackage);
        }


        var getById = new XAction
        {
            Name = $"Get{entity.Name}ById",
            Package = queryPackage,
            BehaviourType = XBehaviourType.GetOne,
            Entity = entity,
            EntityReference = entity.ToString(),
            Namespace = queryPackage.GetNamespace()
        };

        getById.Response = new XResponse
        {
            Classifier = getById,
            TypeReference = entity.ToString(),
            Type = entity,
            IsCollection = false
        };

        entity.GetIds().ToList().ForEach(x =>
        {
            getById.Parameters.Add(new XParameter
            {
                Name = x.Name,
                Type = x.Type,
                Classifier = getById,
                TypeReference = x.TypeReference,
                IsCollection = false
            });
        });


        queryPackage.Add(getById);
        _typeService.Add(getById);

        var getAll = new XAction
        {
            Name = $"GetAll{entity.PluralName}",
            Package = queryPackage,
            BehaviourType = XBehaviourType.GetMany,
            Entity = entity,
            EntityReference = entity.ToString(),
            Namespace = queryPackage.GetNamespace()
        };

        getAll.Response = new XResponse
        {
            Classifier = getById,
            TypeReference = entity.ToString(),
            Type = entity,
            IsCollection = true
        };

        queryPackage.Add(getAll);
        _typeService.Add(getAll);
    }
}