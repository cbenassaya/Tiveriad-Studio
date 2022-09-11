namespace Tiveriad.Studio.Generators.Projects;

public class DefaultProjectTemplateService:IProjectTemplateService
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
    public string GetPath(string stereotype)
    {
        ArgumentNullException.ThrowIfNull(_projectTemplate, "Project Template is null ! Please load it before");


        var items = _projectTemplate.Components.SelectMany(
            component => component.ComponentItems,
            (component, componentItem) => new { Component = component, ComponentItem = componentItem }

        ).Where(x=>x.ComponentItem.Stereotype==stereotype).ToArray();
        if (items.Length > 1)
        {
            throw new ArgumentException($"More than one definition for {stereotype} stereotype");
        }
        var item = items.FirstOrDefault();
        
        ArgumentNullException.ThrowIfNull(item, $"No definition for {stereotype} stereotype");

        return $"{item.Component.Type}s/{{projectName}}{item.Component.Layer}/{item.ComponentItem.Pattern}";
    }

    /// <summary>
    /// Get all path patterns
    /// </summary>
    /// <returns>IEnumerable<string />
    /// </returns>
    /// <exception cref="ArgumentException"></exception>
    public IEnumerable<string> GetPaths()
    {
        ArgumentNullException.ThrowIfNull(_projectTemplate, "Project Template is null ! Please load it before");
        
        return  _projectTemplate.Components.SelectMany(
            component => component.ComponentItems,
            (component, componentItem) => new { Component = component, ComponentItem = componentItem }

        ).Select(item => $"{item.Component.Type}s/{{projectName}}{item.Component.Layer}/{item.ComponentItem.Pattern}");
    }
}