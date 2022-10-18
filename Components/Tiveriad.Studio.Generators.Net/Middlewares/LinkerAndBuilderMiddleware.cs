using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Net.Sources;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class LinkerAndBuilderMiddleware : 
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private  IList<InternalType> _internalTypes;
    private readonly IList<SourceItem> _sourceItems = new List<SourceItem>();


    public void Run(PipelineContext context, PipelineModel model)
    {
        _internalTypes = context.Properties.InternalTypes as IList<InternalType> ?? new List<InternalType>();
        foreach (var internalType in _internalTypes)
            if (ApplyIf(internalType))
                DoApply(internalType);
        context.Properties.SourceItems = _sourceItems;
    }

    private bool ApplyIf(InternalType value)
    {
        return value is
            Class or
            Record or
            Interface or
            Enumeration;
    }

    private void DoApply(InternalType value)
    {
        if (value is Class @class)
            DoApply(@class);

        if (value is Record record)
            DoApply(record);

        if (value is Interface @interface)
            DoApply(@interface);

        if (value is Enumeration enumeration)
            DoApply(enumeration);
    }

    private  void DoApply(Class item)
    {
        NamespaceProcessor.UpdateDependencies(item,_internalTypes);
        _sourceItems.Add(new SourceItem(item, item.ToSourceCode()));
    }
    private  void DoApply(Record item)
    {
        NamespaceProcessor.UpdateDependencies(item,_internalTypes);
        _sourceItems.Add(new SourceItem(item, item.ToSourceCode()));
    }
    private  void DoApply(Interface item)
    {
        NamespaceProcessor.UpdateDependencies(item,_internalTypes);
        _sourceItems.Add(new SourceItem(item, item.ToSourceCode()));
    }
    private  void DoApply(Enumeration item)
    {
        NamespaceProcessor.UpdateDependencies(item,_internalTypes);
        _sourceItems.Add(new SourceItem(item, item.ToSourceCode()));
    }
   
}