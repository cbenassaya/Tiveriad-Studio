using Microsoft.Extensions.Logging;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;

namespace Tiveriad.Studio.Application.Middlewares;

public class PostLoadingMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    
    private readonly ILogger<PostLoadingMiddleware> _logger;

    public PostLoadingMiddleware(ILogger<PostLoadingMiddleware> logger)
    {
        _logger = logger;
    }

    public Task Run(PipelineContext context, PipelineModel model)
    {
        Traverse(model.Project);
        return Task.CompletedTask;
    }

    protected override bool ApplyIf(XElementBase value)
    {
        return value is XProject or XModule or XPackage or XClassifier or XEntity or XEndPoint or XAction
            or XService;
    }

    protected override void DoApply(XElementBase value)
    {
        if (value is XProject project)
            DoApply(project);

        if (value is XModule module)
            DoApply(module);

        if (value is XPackage package)
            DoApply(package);

        if (value is XClassifier classifier)
            DoApply(classifier);

        if (value is XEntity entity)
            DoApply(entity);

        if (value is XService service)
            DoApply(service);

        if (value is XEndPoint endPoint)
            DoApply(endPoint);

        if (value is XAction action)
            DoApply(action);
    }

    private void DoApply(XProject project)
    {
        foreach (var module in project.Modules) module.Project = project;
    }

    private void DoApply(XModule module)
    {
        foreach (var xPackage in module.Packages) xPackage.Module = module;
    }

    private void DoApply(XPackage package)
    {
        foreach (var xPackage in package.GetPackages()) xPackage.Parent = package;
        var types = package.GetEntities().Select(t => t).Cast<XType>().ToList();
        types.AddRange(package.GetEnums().Select(t => t).Cast<XType>().ToList());
        types.AddRange(package.GetActions().Select(t => t).Cast<XType>().ToList());
        types.AddRange(package.GetEndPoints().Select(t => t).Cast<XType>().ToList());
        types.AddRange(package.GetContracts().Select(t => t).Cast<XType>().ToList());
        types.ForEach(item =>
        {
            item.Package = package;
            item.Namespace = package.GetNamespace();
        });
    }

    private void DoApply(XClassifier classifier)
    {
        foreach (var member in classifier.Properties) member.Classifier = classifier;

        foreach (var constraint in classifier.Properties.SelectMany(x => x.Constraints).ToList())
            constraint.Classifier = classifier;
    }

    private void DoApply(XEntity entity)
    {
        foreach (var relationShip in entity.RelationShips) relationShip.Classifier = entity;

        if (entity.Persistence != null)
            entity.Persistence.Entity = entity;
    }

    private void DoApply(XService service)
    {
        foreach (var parameter in service.Parameters) parameter.Classifier = service;
        if (service?.Response != null)
            service.Response.Classifier = service;
    }


    private void DoApply(XAction action)
    {
        foreach (var condition in action.PreConditions)
        {
            condition.RuleFor = action.GetFromPath(condition.Path);

            condition.Action = action;
        }

        foreach (var condition in action.PostConditions)
        {
            condition.RuleFor = action.GetFromPath(condition.Path);
            condition.Action = action;
        }
    }
}