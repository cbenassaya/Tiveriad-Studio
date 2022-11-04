using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Net.Sources;
using Tiveriad.Studio.Generators.Services;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class LinkerAndBuilderMiddleware : 
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IProjectTemplateService<InternalType, ProjectDefinition> _defaultProjectTemplateService;
    public LinkerAndBuilderMiddleware(IProjectTemplateService<InternalType, ProjectDefinition> defaultProjectTemplateService)
    {
        _defaultProjectTemplateService = defaultProjectTemplateService;
    }


    public Task Run(PipelineContext context, PipelineModel model)
    {
        var internalTypes = context.Properties.GetOrAdd("InternalTypes", ()=> new List<InternalType>()) as IList<InternalType>;
        var sourcesItems = context.Properties.GetOrAdd("SourceItems", ()=> new List<SourceItem>()) as IList<SourceItem>;
        foreach (var internalType in internalTypes)
            if (ApplyIf(internalType))
            {
                NamespaceProcessor.UpdateDependencies(internalType,internalTypes);
                var item = DoApply(internalType);
                if (item!=null)
                    sourcesItems.Add(item);
            }
        return Task.CompletedTask;
    }

    private bool ApplyIf(InternalType value)
    {
        return value is
            Class or
            Record or
            Interface or
            Enumeration;
    }

    private SourceItem? DoApply(InternalType value)
    {
        if (value is Class @class)
            return DoApply(@class);

        if (value is Record record)
            return DoApply(record);

        if (value is Interface @interface)
            return DoApply(@interface);

        if (value is Enumeration enumeration)
            return DoApply(enumeration);
        return null;
    }

    private  SourceItem DoApply(Class item)=> SourceItem.Init().WithName($"{item.Name}.cs")
        .WithDirectory(_defaultProjectTemplateService.GetItemPath(item)).WithSource(item.ToSourceCode());
    private  SourceItem DoApply(Record item)=> SourceItem.Init().WithName($"{item.Name}.cs")
        .WithDirectory(_defaultProjectTemplateService.GetItemPath(item)).WithSource(item.ToSourceCode());
    private  SourceItem DoApply(Interface item)=> SourceItem.Init().WithName($"{item.Name}.cs")
        .WithDirectory(_defaultProjectTemplateService.GetItemPath(item)).WithSource(item.ToSourceCode());
    private SourceItem DoApply(Enumeration item) => SourceItem.Init().WithName($"{item.Name}.cs")
        .WithDirectory(_defaultProjectTemplateService.GetItemPath(item)).WithSource(item.ToSourceCode());

}