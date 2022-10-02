using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Net.Extensions;
using Tiveriad.Studio.Generators.Net.Sources;
using Tiveriad.Studio.Generators.Net.Transformers;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class NetCodeBuilderMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IList<SourceItem> _sourceItems = new List<SourceItem>();

    private ISender _sender;

    public NetCodeBuilderMiddleware(ISender sender)
    {
        _sender = sender;
    }

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


    private async void DoApply(XEnum @enum)
    {
        
        var builder = await _sender.Send(new EnumBuilderRequest(@enum))
                      ?? throw new NullReferenceException("EnumBuilder is null");
        builder.WithStereotype("Enum");
        
        var internalType = builder.Build();
        _sourceItems.Add(new SourceItem(internalType, internalType.ToSourceCode()));
    }

    private async void DoApply(XEntity item)
    {
        var entityBuilder = await _sender.Send(new EntityBuilderRequest(item))
                            ?? throw new NullReferenceException("EntityBuilder is null");
        entityBuilder.WithStereotype("Entity");
        var entity = entityBuilder.Build();
        _sourceItems.Add(new SourceItem(entity, entity.ToSourceCode()));
        
        var persistenceBuilder = await _sender.Send(new PersistenceBuilderRequest(item))
                                 ?? throw new NullReferenceException("PersistenceBuilder is null");
        persistenceBuilder.WithStereotype("Persistence");
        var persistence = persistenceBuilder.Build();
        _sourceItems.Add(new SourceItem(persistence, persistence.ToSourceCode()));
    }

    private void DoApply(XService entity)
    {
    }

    private async void DoApply(XEndPoint endPoint)
    {
        var builder = await _sender.Send(new EndPointBuilderRequest(endPoint))
                      ?? throw new NullReferenceException("EndPointBuilder is null");
        builder.WithStereotype("Endpoint");
        var internalType = builder.Build();
        _sourceItems.Add(new SourceItem(internalType, internalType.ToSourceCode()));
    }

    private async void DoApply(XAction entity)
    {
        var recordBuilder = await _sender.Send(new RequestBuilderRequest(entity))
            ?? throw new NullReferenceException("RequestBuilder is null");
        recordBuilder.WithStereotype($"{entity.BehaviourType.ToCqrs()}Request");
        var record = recordBuilder.Build();
        _sourceItems.Add(new SourceItem(record, record.ToSourceCode()));
        
        var actionBuilder = await _sender.Send(new ActionBuilderRequest(entity,record))
                            ?? throw new NullReferenceException("ActionBuilder is null");
        actionBuilder.WithStereotype($"{entity.BehaviourType.ToCqrs()}Action");
        var action = actionBuilder.Build();
        _sourceItems.Add(new SourceItem(action, action.ToSourceCode()));
        
        
        var validatorBuilder = await _sender.Send(new ValidatorBuilderRequest(entity,record))
                            ?? throw new NullReferenceException("ActionBuilder is null");
        validatorBuilder.WithStereotype($"{entity.BehaviourType.ToCqrs()}Validator");
        var validator = validatorBuilder.Build();
        _sourceItems.Add(new SourceItem(validator, validator.ToSourceCode()));
    }

    private async void DoApply(XContract contract)
    {
        var builder = await _sender.Send(new ContractBuilderRequest(contract))
                      ?? throw new NullReferenceException("ContractBuilder is null");
        
        builder.WithStereotype("Contract");
        var internalType = builder.Build();
        _sourceItems.Add(new SourceItem(internalType, internalType.ToSourceCode()));
    }
}