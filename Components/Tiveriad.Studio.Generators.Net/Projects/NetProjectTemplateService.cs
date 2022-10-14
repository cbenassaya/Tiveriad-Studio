using Optional.Unsafe;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Projects;
using Tiveriad.Studio.Generators.Services;

namespace Tiveriad.Studio.Generators.Net.Projects;


public record ProjectDefinition(string ProjectName, string ProjectPath, string ProjectTemplate, string Layer, XModule Module);

public class NetProjectTemplateService : IProjectTemplateService<InternalType,ProjectDefinition>
{
    private readonly ProjectDefinitionTemplate _projectDefinitionTemplate;


    public NetProjectTemplateService(ProjectDefinitionTemplate projectDefinitionTemplate)
    {
        ArgumentNullException.ThrowIfNull(projectDefinitionTemplate, "Project Template is null ! Please load it before");
        _projectDefinitionTemplate = projectDefinitionTemplate;
    }

    public IEnumerable<ProjectDefinition> GetProjects( XProject project)
    {
        ArgumentNullException.ThrowIfNull(project, "XProject");
        return _projectDefinitionTemplate.ComponentDefinitions.SelectMany(x => project.Modules.Select(y => 
                new ProjectDefinition(
                    string.Format(x.NamePattern,y.Project.RootNamespace,y.Name), 
                    $"{x.Type}s{Path.DirectorySeparatorChar}{string.Format(x.NamePattern,y.Project.RootNamespace,y.Name)}",
                    x.Template,
                    x.Layer,
                    y)))
            .ToList();
    }

    public IEnumerable<string> GetReferences(ProjectDefinition projectDefinition)
    {
        var references = _projectDefinitionTemplate.ComponentDefinitions.Where(x=>x.Layer==projectDefinition.Layer).SelectMany(x => x.References.Select(y=> y.Name)).ToList();

        return  _projectDefinitionTemplate.ComponentDefinitions.Where(x => references.Contains(x.Layer)).Select(x =>
            $"{x.Type}s{Path.DirectorySeparatorChar}{string.Format(x.NamePattern, projectDefinition.Module.Project.RootNamespace, projectDefinition.Module.Name)}").ToList();
    }

    public string GetLayer(string stereotype)
    {
        var items = _projectDefinitionTemplate.ComponentDefinitions.SelectMany(
            component => component.ComponentItems,
            (component, componentItem) => new { Component = component, ComponentItem = componentItem }
        ).Where(x => x.ComponentItem.Stereotype == stereotype).ToArray();
        if (items.Length > 1)
            throw new ArgumentException(
                $"More than one definition for {stereotype} stereotype");
        var item = items.FirstOrDefault();
        ArgumentNullException.ThrowIfNull(item,
            $"No definition for {stereotype} stereotype");

        return item.Component.Layer;
    }

    public IEnumerable<Dependency> GetDependencies(ProjectDefinition projectDefinition)
    {
        ArgumentNullException.ThrowIfNull(projectDefinition.Layer, "layer");
        return _projectDefinitionTemplate.ComponentDefinitions.Where(x=>x.Layer==projectDefinition.Layer).SelectMany(x => x.Dependencies);
    }


    /// <summary>
    ///     Get path pattern
    /// </summary>
    /// <param name="stereotype"></param>
    /// <returns>string</returns>
    /// <exception cref="ArgumentException"></exception>
    public string GetItemPath(InternalType internalType)
    {
        ArgumentNullException.ThrowIfNull(internalType, "InternalType");
        var classifier = internalType.Reference.ValueOrFailure();
        var module = classifier.GetModule();
        var project = classifier.GetProject();
        ArgumentNullException.ThrowIfNull(project, "XProject");
        ArgumentNullException.ThrowIfNull(module, "XModule");
        

        var items = _projectDefinitionTemplate.ComponentDefinitions.SelectMany(
            component => component.ComponentItems,
            (component, componentItem) => new { Component = component, ComponentItem = componentItem }
        ).Where(x => x.ComponentItem.Stereotype == internalType.Stereotype.ValueOrFailure()).ToArray();
        if (items.Length > 1)
            throw new ArgumentException(
                $"More than one definition for {internalType.Stereotype.ValueOrFailure()} stereotype");
        var item = items.FirstOrDefault();

        ArgumentNullException.ThrowIfNull(item,
            $"No definition for {internalType.Stereotype.ValueOrFailure()} stereotype");
        var @namespace = internalType.Namespace.ValueOrFailure();
        var subNamespace = @namespace.Substring($"{string.Format(item.Component.NamePattern, project.RootNamespace, module.Name)}.".Length);
        var partialPath = classifier.GetPartialNamespace().Replace('.', Path.DirectorySeparatorChar);
        var componentPath =
            $"{item.Component.Type}s{Path.DirectorySeparatorChar}{string.Format(item.Component.NamePattern, project.RootNamespace, module.Name)}";
        return Path.Combine(componentPath,partialPath);
    }
    

}