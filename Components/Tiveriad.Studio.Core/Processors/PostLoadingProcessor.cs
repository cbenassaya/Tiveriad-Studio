using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;

namespace Tiveriad.Studio.Core.Processors;

public class PostLoadingProcessor : AbstractProcessor<XElementBase, XNamedElement>, IProcessor
{
    private readonly XTypeLoader _typeLoader;

    public PostLoadingProcessor(XTypeLoader typeLoader)
    {
        _typeLoader = typeLoader;
    }

    protected override bool ApplyIf(XElementBase value)
    {
        return value is XProject or XComponent or XPackage or XClassifier or XEntity or XEndPoint or XAction
            or XService;
    }

    protected override void DoApply(XElementBase value)
    {
        if (value is XProject project)
            DoApply(project);

        if (value is XComponent component)
            DoApply(component);

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
        foreach (var component in project.Components) component.Project = project;
    }

    private void DoApply(XComponent component)
    {
        foreach (var xPackage in component.Packages) xPackage.Component = component;
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