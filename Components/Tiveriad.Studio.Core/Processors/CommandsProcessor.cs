using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;

namespace Tiveriad.Studio.Core.Processors;

public class CommandsProcessor : AbstractProcessor<XElementBase, XNamedElement>, IProcessor
{
    private readonly XTypeLoader _typeLoader;

    public CommandsProcessor(XTypeLoader typeLoader)
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

        var commandsPackage = component.Packages.FirstOrDefault(x => x.Name == "Commands");
        if (commandsPackage == null)
        {
            commandsPackage = new XPackage
            {
                Name = "Commands",
                Component = component
            };
            component.Packages.Add(commandsPackage);
        }

        var requestPackage = component.Packages.FirstOrDefault(x => x.Name == $"{entity.Name}Requests");
        if (requestPackage == null)
        {
            requestPackage = new XPackage
            {
                Name = $"{entity.Name}Commands",
                Parent = commandsPackage
            };
            commandsPackage.Add(requestPackage);
        }

        var deleteAction = new XAction
        {
            Name = $"Delete{entity.Name}ById",
            Package = requestPackage,
            BehaviourType = XBehaviourType.Delete,
            Entity = entity,
            EntityReference = entity.ToString(),
            Namespace = requestPackage.GetNamespace()
        };

        deleteAction.Response = new XResponse
        {
            Classifier = deleteAction,
            TypeReference = entity.ToString(),
            Type = XDataTypes.BOOL,
            IsCollection = false
        };

        deleteAction.Parameters = entity.Properties.Where(x => x is XId).Select(x => new XParameter
        {
            Name = x.Name,
            Type = x.Type,
            Classifier = deleteAction,
            TypeReference = x.TypeReference,
            IsCollection = false,
        }).ToList();

        requestPackage.Add(deleteAction);
        _typeLoader.Add(deleteAction);

        var saveOrUpdateAction = new XAction
        {
            Name = $"SaveOrUpdate{entity.Name}",
            Package = requestPackage,
            BehaviourType = XBehaviourType.SaveOrUpdate,
            Entity = entity,
            EntityReference = entity.ToString(),
            Namespace = requestPackage.GetNamespace()
        };

        saveOrUpdateAction.Parameters = new List<XParameter>
        {
            new()
            {
                Name = entity.Name,
                Type = entity,
                Classifier = saveOrUpdateAction,
                TypeReference = entity.ToString(),
                IsCollection = false,
            }
        };

        saveOrUpdateAction.Response = new XResponse
        {
            Classifier = saveOrUpdateAction,
            TypeReference = entity.ToString(),
            Type = entity,
            IsCollection = false
        };

        requestPackage.Add(saveOrUpdateAction);
        _typeLoader.Add(saveOrUpdateAction);
    }
}