using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;

namespace Tiveriad.Studio.Core.Processors;

public class QueriesProcessor : AbstractProcessor<XElementBase, XNamedElement>, IProcessor
{
    private readonly XTypeLoader _typeLoader;

    public QueriesProcessor(XTypeLoader typeLoader)
    {
        _typeLoader = typeLoader;
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

        var component = project.Components.FirstOrDefault(x => x.Name == "Application");
        if (component == null)
        {
            component = new XComponent
            {
                Name = "Application",
                Project = project
            };
            project.Components.Add(component);
        }

        var queriesPackage = component.Packages.FirstOrDefault(x => x.Name == "Queries");
        if (queriesPackage == null)
        {
            queriesPackage = new XPackage
            {
                Name = "Queries",
                Component = component
            };
            component.Packages.Add(queriesPackage);
        }

        var queryPackage = component.Packages.FirstOrDefault(x => x.Name == $"{entity.Name}Queries");
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
                IsCollection = false,
            });
        });


        queryPackage.Add(getById);
        _typeLoader.Add(getById);

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
        _typeLoader.Add(getAll);
    }
}