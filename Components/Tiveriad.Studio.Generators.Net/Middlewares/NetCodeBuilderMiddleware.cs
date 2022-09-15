using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Net.Extensions;
using Tiveriad.Studio.Generators.Net.Sources;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class NetCodeBuilderMiddleware: AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{

    private IList<SourceItem> _sourceItems = new List<SourceItem>();
    
    public void Run(PipelineContext context, PipelineModel model)
    {
        ArgumentNullException.ThrowIfNull("IInternalTypeFormatter");
        Traverse(model.Project);
        context.Properties.SourceItems = _sourceItems;
    }
    
    protected override bool ApplyIf(XElementBase value)
    {
        return value is 
            XEnum or 
            XEntity or 
            XEndPoint or 
            XAction or 
            XService or
            XContract;
    }
    
    protected override void DoApply(XElementBase value)
    {
        if (value is XEnum @enum)
            DoApply(@enum);
        
        if (value is XEntity entity)
            DoApply(entity);

        if (value is XService service)
            DoApply(service);

        if (value is XEndPoint endPoint)
            DoApply(endPoint);

        if (value is XAction action)
            DoApply(action);
        
        if (value is XContract contract)
            DoApply(contract);
    }

        
    private void DoApply(XEnum @enum)
    {
        var builder = @enum.ToBuilder().WithStereotype("Enum");
        var internalType = builder.Build();
        _sourceItems.Add(new SourceItem(internalType,internalType.ToSourceCode()));
    }

    private void DoApply(XEntity item)
    {
        var entityBuilder = item.ToBuilder().WithStereotype("Entity");
        var entity = entityBuilder.Build();
        _sourceItems.Add(new SourceItem(entity,entity.ToSourceCode()));
        var persistenceBuilder = item.ToPersistenceBuilder().WithStereotype("Persistence");
        var persistence = persistenceBuilder.Build();
        _sourceItems.Add(new SourceItem(persistence,persistence.ToSourceCode()));
    }
    
    private void DoApply(XService entity)
    {
    }
    
    private void DoApply(XEndPoint endPoint)
    {

        var builder = endPoint.ToBuilder().WithStereotype("Endpoint");
        var internalType = builder.Build();
        _sourceItems.Add(new SourceItem(internalType,internalType.ToSourceCode()));
    }
    
    private void DoApply(XAction entity)
    {
        var recordBuilder = entity.ToRequestBuilder().WithStereotype($"{entity.BehaviourType.ToCqrs()}Request");
        var record = recordBuilder.Build();
        _sourceItems.Add(new SourceItem(record,record.ToSourceCode()));
        var actionBuilder = entity.ToActionBuilder(recordBuilder.Build()).WithStereotype($"{entity.BehaviourType.ToCqrs()}Action");
        var actionB = actionBuilder.Build();
        _sourceItems.Add(new SourceItem(actionB,actionB.ToSourceCode()));
        var validatorBuilder = entity.ToValidatorBuilder(recordBuilder.Build()).WithStereotype($"{entity.BehaviourType.ToCqrs()}Validator");
        var validator = validatorBuilder.Build();
        _sourceItems.Add(new SourceItem(validator,validator.ToSourceCode()));
    }
    
    private void DoApply(XContract contract)
    {
        var builder = contract.ToBuilder().WithStereotype("Contract");
        var internalType = builder.Build();
        _sourceItems.Add(new SourceItem(internalType,internalType.ToSourceCode()));
    }
    
}