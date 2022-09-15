using Optional.Unsafe;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Services;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Services;

namespace Tiveriad.Studio.Generators.Projects;

public class DefaultProjectTemplateService:IProjectTemplateService<InternalType>
{
    private ProjectTemplate _projectTemplate;


    public DefaultProjectTemplateService(ProjectTemplate projectTemplate)
    {
        _projectTemplate = projectTemplate;
    }

    /// <summary>
    /// Get path pattern
    /// </summary>
    /// <param name="stereotype"></param>
    /// <returns>string</returns>
    /// <exception cref="ArgumentException"></exception>
    public string GetPath(InternalType internalType, XProject project)
    {
        ArgumentNullException.ThrowIfNull(_projectTemplate, "Project Template is null ! Please load it before");
        ArgumentNullException.ThrowIfNull(internalType, "InternalType");
        ArgumentNullException.ThrowIfNull(project, "XProject");

        var items = _projectTemplate.Components.SelectMany(
            component => component.ComponentItems,
            (component, componentItem) => new { Component = component, ComponentItem = componentItem }

        ).Where(x=>x.ComponentItem.Stereotype==internalType.Stereotype.ValueOrFailure()).ToArray();
        if (items.Length > 1)
        {
            throw new ArgumentException($"More than one definition for {internalType.Stereotype.ValueOrFailure()} stereotype");
        }
        var item = items.FirstOrDefault();
        
        ArgumentNullException.ThrowIfNull(item, $"No definition for {internalType.Stereotype.ValueOrFailure()} stereotype");
        var @namespace = internalType.Namespace.ValueOrFailure();
        var subNamespace = @namespace.Substring($"{project.RootNamespace}.{item.Component.Layer}.".Length);
        var partialPath = subNamespace.Replace('.', Path.DirectorySeparatorChar);
        return  $"{item.Component.Type}s/{project.RootNamespace}.{item.Component.Layer}/{partialPath}";
    }
    
    

}