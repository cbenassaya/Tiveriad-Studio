using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class CommandMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IXTypeService _typeService;

    public CommandMiddleware(IXTypeService typeService)
    {
        _typeService = typeService;
    }

    public void Run(PipelineContext context, PipelineModel model)
    {
        Traverse(model.Project);
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

        var commandsPackage = module.Packages.FirstOrDefault(x => x.Name == "Commands");
        if (commandsPackage == null)
        {
            commandsPackage = new XPackage
            {
                Name = "Commands",
                Module = module
            };
            module.Packages.Add(commandsPackage);
        }

        var commandPackage = module.Packages.FirstOrDefault(x => x.Name == $"{entity.Name}Commands");
        if (commandPackage == null)
        {
            commandPackage = new XPackage
            {
                Name = $"{entity.Name}Commands",
                Parent = commandsPackage
            };
            commandsPackage.Add(commandPackage);
        }

        var deleteAction = new XAction
        {
            Name = $"Delete{entity.Name}ById",
            Package = commandPackage,
            BehaviourType = XBehaviourType.Delete,
            Entity = entity,
            EntityReference = entity.ToString(),
            Namespace = commandPackage.GetNamespace()
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
            IsCollection = false
        }).ToList();

        commandPackage.Add(deleteAction);
        _typeService.Add(deleteAction);

        var saveOrUpdateAction = new XAction
        {
            Name = $"SaveOrUpdate{entity.Name}",
            Package = commandPackage,
            BehaviourType = XBehaviourType.SaveOrUpdate,
            Entity = entity,
            EntityReference = entity.ToString(),
            Namespace = commandPackage.GetNamespace()
        };

        saveOrUpdateAction.Parameters = new List<XParameter>
        {
            new()
            {
                Name = entity.Name,
                Type = entity,
                Classifier = saveOrUpdateAction,
                TypeReference = entity.ToString(),
                IsCollection = false
            }
        };

        saveOrUpdateAction.Response = new XResponse
        {
            Classifier = saveOrUpdateAction,
            TypeReference = entity.ToString(),
            Type = entity,
            IsCollection = false
        };

        commandPackage.Add(saveOrUpdateAction);
        _typeService.Add(saveOrUpdateAction);
    }
}